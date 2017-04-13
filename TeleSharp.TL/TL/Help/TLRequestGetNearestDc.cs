using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(531836966)]
    public class TLRequestGetNearestDc : TLMethod
    {
        public override int Constructor => 531836966;

        public TLNearestDc Response { get; set; }


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
            Response = (TLNearestDc) ObjectUtils.DeserializeObject(br);
        }
    }
}