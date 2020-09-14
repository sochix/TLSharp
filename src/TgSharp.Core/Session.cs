using System;
using System.IO;

using TgSharp.TL;
using TgSharp.Core.MTProto;
using TgSharp.Core.MTProto.Crypto;

namespace TgSharp.Core
{
    public interface ISessionStore
    {
        void Save(Session session);
        Session Load(string sessionUserId);
    }

    public class FileSessionStore : ISessionStore
    {
        private readonly DirectoryInfo basePath;

        public FileSessionStore(DirectoryInfo basePath = null)
        {
            if (basePath != null && !basePath.Exists)
            {
                throw new ArgumentException("basePath doesn't exist", nameof(basePath));
            }
            this.basePath = basePath;
        }

        public void Save(Session session)
        {
            string sessionFileName = $"{session.SessionUserId}.dat";
            var sessionPath = basePath == null ? sessionFileName :
                Path.Combine(basePath.FullName, sessionFileName);

            using (var stream = new FileStream(sessionPath, FileMode.OpenOrCreate))
            {
                var result = session.ToBytes();
                stream.Write(result, 0, result.Length);
            }
        }

        public Session Load(string sessionUserId)
        {
            string sessionFileName = $"{sessionUserId}.dat";
            var sessionPath = basePath == null ? sessionFileName :
                Path.Combine(basePath.FullName, sessionFileName);

            if (!File.Exists(sessionPath))
                return null;

            using (var stream = new FileStream(sessionPath, FileMode.Open))
            {
                var buffer = new byte[2048];
                stream.Read(buffer, 0, 2048);

                return Session.FromBytes(buffer, this, sessionUserId);
            }
        }
    }

    public class FakeSessionStore : ISessionStore
    {
        public void Save(Session session)
        {

        }

        public Session Load(string sessionUserId)
        {
            return null;
        }
    }

    internal static class SessionFactory
    {
        internal static Session TryLoadOrCreateNew (ISessionStore store, string sessionUserId)
        {
            var session = store.Load (sessionUserId);
            if (null == session) {
                var defaultDataCenter = new DataCenter ();
                session = new Session {
                    Id = GenerateRandomUlong (),
                    SessionUserId = sessionUserId,
                    DataCenter = defaultDataCenter,
                };
            }
            return session;
        }

        private static ulong GenerateRandomUlong ()
        {
            var random = new Random ();
            ulong rand = (((ulong)random.Next ()) << 32) | ((ulong)random.Next ());
            return rand;
        }
    }

    public class Session
    {
        public int Sequence { get; set; }
#if CI
            // see the same CI-wrapped assignment in .FromBytes(), but this one will become useful
            // when we generate a new session.dat for CI again
            = CurrentTime ();

        // this is similar to the unixTime but rooted on the worst year of humanity instead of 1970
        private static int CurrentTime ()
        {
            return (int)DateTime.UtcNow.Subtract (new DateTime (2020, 1, 1)).TotalSeconds;
        }
#endif

        public string SessionUserId { get; set; }
        internal DataCenter DataCenter { get; set; }
        public AuthKey AuthKey { get; set; }
        public ulong Id { get; set; }
        public ulong Salt { get; set; }
        public int TimeOffset { get; set; }
        public long LastMessageId { get; set; }
        public int SessionExpires { get; set; }
        public TLUser TLUser { get; set; }
        private Random random;

        public Session()
        {
            random = new Random();
        }

        public byte[] ToBytes()
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(Id);
                writer.Write(Sequence);
                writer.Write(Salt);
                writer.Write(LastMessageId);
                writer.Write(TimeOffset);
                Serializers.String.Write(writer, DataCenter.Address);
                writer.Write(DataCenter.Port);

                if (TLUser != null)
                {
                    writer.Write(1);
                    writer.Write(SessionExpires);
                    ObjectUtils.SerializeObject(TLUser, writer);
                }
                else
                {
                    writer.Write(0);
                }

                Serializers.Bytes.Write(writer, AuthKey.Data);

                return stream.ToArray();
            }
        }

        public static Session FromBytes(byte[] buffer, ISessionStore store, string sessionUserId)
        {
            using (var stream = new MemoryStream(buffer))
            using (var reader = new BinaryReader(stream))
            {
                var id = reader.ReadUInt64();
                var sequence = reader.ReadInt32();

// we do this in CI when running tests so that the they can always use a
// higher sequence than previous run
#if CI
                sequence = CurrentTime();
#endif

                var salt = reader.ReadUInt64();
                var lastMessageId = reader.ReadInt64();
                var timeOffset = reader.ReadInt32();
                var serverAddress = Serializers.String.Read(reader);
                var port = reader.ReadInt32();

                var isAuthExsist = reader.ReadInt32() == 1;
                int sessionExpires = 0;
                TLUser TLUser = null;
                if (isAuthExsist)
                {
                    sessionExpires = reader.ReadInt32();
                    TLUser = (TLUser)ObjectUtils.DeserializeObject(reader);
                }

                var authData = Serializers.Bytes.Read(reader);
                var defaultDataCenter = new DataCenter (serverAddress, port);

                return new Session()
                {
                    AuthKey = new AuthKey(authData),
                    Id = id,
                    Salt = salt,
                    Sequence = sequence,
                    LastMessageId = lastMessageId,
                    TimeOffset = timeOffset,
                    SessionExpires = sessionExpires,
                    TLUser = TLUser,
                    SessionUserId = sessionUserId,
                    DataCenter = defaultDataCenter,
                };
            }
        }

        public long GetNewMessageId()
        {
            long time = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
            long newMessageId = ((time / 1000 + TimeOffset) << 32) |
                                ((time % 1000) << 22) |
                                (random.Next(524288) << 2); // 2^19
                                                            // [ unix timestamp : 32 bit] [ milliseconds : 10 bit ] [ buffer space : 1 bit ] [ random : 19 bit ] [ msg_id type : 2 bit ] = [ msg_id : 64 bit ]

            if (LastMessageId >= newMessageId)
            {
                newMessageId = LastMessageId + 4;
            }

            LastMessageId = newMessageId;
            return newMessageId;
        }
    }
}
