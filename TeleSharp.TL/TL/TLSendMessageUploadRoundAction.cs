using System.IO;

namespace TeleSharp.TL
{
    [TLObject(608050278)]
    public class TLSendMessageUploadRoundAction : TLAbsSendMessageAction
    {
        public override int Constructor
        {
            get
            {
                return 608050278;
            }
        }

        public int progress { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            progress = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(progress);
        }
    }
}