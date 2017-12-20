using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-283684427)]
    public class TLPageBlockChannel : TLAbsPageBlock
    {
        public TLAbsChat Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return -283684427;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsChat)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
        }
    }
}
