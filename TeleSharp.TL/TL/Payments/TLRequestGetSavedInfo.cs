using System.IO;

namespace TeleSharp.TL.Payments
{
    [TLObject(578650699)]
    public class TLRequestGetSavedInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 578650699;
            }
        }

        public Payments.TLSavedInfo Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Payments.TLSavedInfo)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }
    }
}
