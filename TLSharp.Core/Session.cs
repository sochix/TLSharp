using System;
using System.IO;
using TeleSharp.TL;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;

namespace TLSharp.Core
{
    public interface ISessionStore
    {
        void Save(Session session);
        Session Load(string sessionUserId);
    }

    public class FileSessionStore : ISessionStore
    {
        public void Save(Session session)
        {
            using (var stream = new FileStream($"{session.SessionUserId}.dat", FileMode.OpenOrCreate))
            {
                var result = session.ToBytes();
                stream.Write(result, 0, result.Length);
            }
        }

        public Session Load(string sessionUserId)
        {
            var sessionFileName = $"{sessionUserId}.dat";
            if (!File.Exists(sessionFileName))
                return null;

            using (var stream = new FileStream(sessionFileName, FileMode.Open))
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

    public class Session
    {
	    private const string defaultConnectionAddress = "149.154.175.100";//"149.154.167.50";

		private const int defaultConnectionPort = 443;

        public string SessionUserId { get; set; }
        public string ServerAddress { get; set; }
        public int Port { get; set; }
        public AuthKey AuthKey { get; set; }
        public ulong Id { get; set; }
        public int Sequence { get; set; }
        public ulong Salt { get; set; }
        public int TimeOffset { get; set; }
        public long LastMessageId { get; set; }
        public int SessionExpires { get; set; }
        public TLUser TLUser { get; set; }
        private Random random;

        private ISessionStore _store;

        public Session(ISessionStore store)
        {
            random = new Random();
            _store = store;
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
                Serializers.String.write(writer, ServerAddress);
                writer.Write(Port);

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

                Serializers.Bytes.write(writer, AuthKey.Data);

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
                var salt = reader.ReadUInt64();
                var lastMessageId = reader.ReadInt64();
                var timeOffset = reader.ReadInt32();
                var serverAddress = Serializers.String.read(reader);
                var port = reader.ReadInt32();

                var isAuthExsist = reader.ReadInt32() == 1;
                int sessionExpires = 0;
                TLUser TLUser = null;
                if (isAuthExsist)
                {
                    sessionExpires = reader.ReadInt32();
                    TLUser = (TLUser)ObjectUtils.DeserializeObject(reader);
                }

                var authData = Serializers.Bytes.read(reader);

                return new Session(store)
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
                    ServerAddress = serverAddress,
                    Port = port
                };
            }
        }

        public void Save()
        {
            _store.Save(this);
        }

        public static Session TryLoad(ISessionStore store, string sessionUserId)
        {
            return store.Load(sessionUserId);
        }

        public static Session Create(
            ISessionStore store, 
            string sessionUserId, 
            string serverAddress = defaultConnectionAddress, 
            int serverPort = defaultConnectionPort
        )
        {
            return new Session(store)
            {
                Id = GenerateRandomUlong(),
                SessionUserId = sessionUserId,
                ServerAddress = serverAddress,
                Port = serverPort
            };
        }

        private static ulong GenerateRandomUlong()
        {
            var random = new Random();
            ulong rand = (((ulong)random.Next()) << 32) | ((ulong)random.Next());
            return rand;
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
