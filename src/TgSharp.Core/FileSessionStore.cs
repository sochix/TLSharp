using System;
using System.IO;

using TgSharp.TL;
using TgSharp.Core.MTProto;
using TgSharp.Core.MTProto.Crypto;

namespace TgSharp.Core
{
    public class FileSessionStore : ISessionStore
    {
        private readonly DirectoryInfo basePath;

        public FileSessionStore (DirectoryInfo basePath = null)
        {
            if (basePath != null && !basePath.Exists) {
                throw new ArgumentException ("basePath doesn't exist", nameof (basePath));
            }
            this.basePath = basePath;
        }

        public void Save (Session session)
        {
            string sessionFileName = $"{session.SessionUserId}.dat";
            var sessionPath = basePath == null ? sessionFileName :
                Path.Combine (basePath.FullName, sessionFileName);

            using (var stream = new FileStream (sessionPath, FileMode.OpenOrCreate)) {
                var result = ToBytes (session);
                stream.Write (result, 0, result.Length);
            }
        }

        public Session Load (string sessionUserId)
        {
            string sessionFileName = $"{sessionUserId}.dat";
            var sessionPath = basePath == null ? sessionFileName :
                Path.Combine (basePath.FullName, sessionFileName);

            if (!File.Exists (sessionPath))
                return null;

            using (var stream = new FileStream (sessionPath, FileMode.Open)) {
                var buffer = new byte [2048];
                stream.Read (buffer, 0, 2048);

                return FromBytes (buffer, this, sessionUserId);
            }
        }

        public static Session FromBytes (byte [] buffer, ISessionStore store, string sessionUserId)
        {
            using (var stream = new MemoryStream (buffer))
            using (var reader = new BinaryReader (stream)) {
                var id = reader.ReadUInt64 ();
                var sequence = reader.ReadInt32 ();

                // we do this in CI when running tests so that the they can always use a
                // higher sequence than previous run
#if CI
                sequence = Session.CurrentTime();
#endif

                var salt = reader.ReadUInt64 ();
                var lastMessageId = reader.ReadInt64 ();
                var timeOffset = reader.ReadInt32 ();
                var serverAddress = Serializers.String.Read (reader);
                var port = reader.ReadInt32 ();

                var doesAuthExist = reader.ReadInt32 () == 1;
                int sessionExpires = 0;
                TLUser TLUser = null;
                if (doesAuthExist) {
                    sessionExpires = reader.ReadInt32 ();
                    TLUser = (TLUser)ObjectUtils.DeserializeObject (reader);
                }

                var authData = Serializers.Bytes.Read (reader);
                var defaultDataCenter = new DataCenter (serverAddress, port);

                return new Session () {
                    AuthKey = new AuthKey (authData),
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

        internal byte [] ToBytes (Session session)
        {
            using (var stream = new MemoryStream ())
            using (var writer = new BinaryWriter (stream)) {
                writer.Write (session.Id);
                writer.Write (session.Sequence);
                writer.Write (session.Salt);
                writer.Write (session.LastMessageId);
                writer.Write (session.TimeOffset);
                Serializers.String.Write (writer, session.DataCenter.Address);
                writer.Write (session.DataCenter.Port);

                if (session.TLUser != null) {
                    writer.Write (1);
                    writer.Write (session.SessionExpires);
                    ObjectUtils.SerializeObject (session.TLUser, writer);
                } else {
                    writer.Write (0);
                }

                Serializers.Bytes.Write (writer, session.AuthKey.Data);

                return stream.ToArray ();
            }
        }
    }
}
