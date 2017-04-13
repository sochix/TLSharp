using System.IO;

namespace TeleSharp.TL
{
    [TLObject(314359194)]
    public class TLUpdateNewEncryptedMessage : TLAbsUpdate
    {
        public override int Constructor => 314359194;

        public TLAbsEncryptedMessage message { get; set; }
        public int qts { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            message = (TLAbsEncryptedMessage) ObjectUtils.DeserializeObject(br);
            qts = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(message, bw);
            bw.Write(qts);
        }
    }
}