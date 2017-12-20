using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-378127636)]
    public class TLSendMessageUploadVideoAction : TLAbsSendMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -378127636;
            }
        }

        public int Progress { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Progress = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Progress);
        }
    }
}
