using System.IO;

namespace TeleSharp.TL.Payments
{
    [TLObject(1800845601)]
    public class TLPaymentVerficationNeeded : TLAbsPaymentResult
    {
        public override int Constructor
        {
            get
            {
                return 1800845601;
            }
        }

        public string Url { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Url = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Url, bw);
        }
    }
}
