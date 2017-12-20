using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(-1663104819)]
    public class TLRequestGetSupport : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1663104819;
            }
        }

        public Help.TLSupport Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Help.TLSupport)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }
    }
}
