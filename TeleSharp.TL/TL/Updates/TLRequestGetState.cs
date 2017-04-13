using System.IO;

namespace TeleSharp.TL.Updates
{
    [TLObject(-304838614)]
    public class TLRequestGetState : TLMethod
    {
        public override int Constructor => -304838614;

        public TLState Response { get; set; }


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
            Response = (TLState) ObjectUtils.DeserializeObject(br);
        }
    }
}