using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-283684427)]
    public class TLPageBlockChannel : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -283684427;
            }
        }

        public TLAbsChat channel { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsChat)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);

        }
    }
}
