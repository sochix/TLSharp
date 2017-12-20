using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-317144808)]
    public class TLEncryptedMessage : TLAbsEncryptedMessage
    {
        public byte[] Bytes { get; set; }

        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return -317144808;
            }
        }

        public int Date { get; set; }

        public TLAbsEncryptedFile File { get; set; }

        public long RandomId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            RandomId = br.ReadInt64();
            ChatId = br.ReadInt32();
            Date = br.ReadInt32();
            Bytes = BytesUtil.Deserialize(br);
            File = (TLAbsEncryptedFile)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(RandomId);
            bw.Write(ChatId);
            bw.Write(Date);
            BytesUtil.Serialize(Bytes, bw);
            ObjectUtils.SerializeObject(File, bw);
        }
    }
}
