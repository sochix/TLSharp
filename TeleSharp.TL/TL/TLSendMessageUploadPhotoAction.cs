using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-774682074)]
    public class TLSendMessageUploadPhotoAction : TLAbsSendMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -774682074;
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
