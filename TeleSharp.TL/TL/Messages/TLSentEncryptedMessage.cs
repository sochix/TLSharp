using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1443858741)]
    public class TLSentEncryptedMessage : TLAbsSentEncryptedMessage
    {
        public override int Constructor => 1443858741;

        public int date { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            date = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(date);
        }
    }
}