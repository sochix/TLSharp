using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(-1663104819)]
    public class TLRequestGetSupport : TLMethod
    {
        public override int Constructor => -1663104819;

        public TLSupport Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLSupport) ObjectUtils.DeserializeObject(br);
        }
    }
}