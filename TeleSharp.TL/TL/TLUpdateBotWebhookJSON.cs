using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2095595325)]
    public class TLUpdateBotWebhookJSON : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -2095595325;
            }
        }

        public TLDataJSON data { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            data = (TLDataJSON)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(data, bw);
        }
    }
}