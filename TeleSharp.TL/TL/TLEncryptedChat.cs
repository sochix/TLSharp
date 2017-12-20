using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-94974410)]
    public class TLEncryptedChat : TLAbsEncryptedChat
    {
        public long AccessHash { get; set; }

        public int AdminId { get; set; }

        public override int Constructor
        {
            get
            {
                return -94974410;
            }
        }

        public int Date { get; set; }

        public byte[] GAOrB { get; set; }

        public int Id { get; set; }

        public long KeyFingerprint { get; set; }

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
            GAOrB = BytesUtil.Deserialize(br);
            KeyFingerprint = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(AccessHash);
            bw.Write(Date);
            bw.Write(AdminId);
            bw.Write(ParticipantId);
            BytesUtil.Serialize(GAOrB, bw);
            bw.Write(KeyFingerprint);
        }
    }
}
