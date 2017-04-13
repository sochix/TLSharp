using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(889286899)]
    public class TLRequestGetTermsOfService : TLMethod
    {
        public override int Constructor => 889286899;

        public TLTermsOfService Response { get; set; }


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
            Response = (TLTermsOfService) ObjectUtils.DeserializeObject(br);
        }
    }
}