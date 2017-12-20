using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1006044124)]
    public class TLEncryptedChatWaiting : TLAbsEncryptedChat
    {
        public long AccessHash { get; set; }

        public int AdminId { get; set; }

        public override int Constructor
        {
            get
            {
                return 1006044124;
            }
        }

        public int Date { get; set; }

        public int Id { get; set; }

        public int ParticipantId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
            AccessHash = br.ReadInt64();
            Date = br.ReadInt32();
            AdminId = br.ReadInt32();
            ParticipantId = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(AccessHash);
            bw.Write(Date);
            bw.Write(AdminId);
            bw.Write(ParticipantId);
        }
    }
}
