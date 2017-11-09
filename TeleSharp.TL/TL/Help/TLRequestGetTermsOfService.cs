using System.IO;
namespace TeleSharp.TL.Help
{
    [TLObject(889286899)]
    public class TLRequestGetTermsOfService : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 889286899;
            }
        }

        public Help.TLTermsOfService Response { get; set; }


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
            Response = (Help.TLTermsOfService)ObjectUtils.DeserializeObject(br);

        }
    }
}
