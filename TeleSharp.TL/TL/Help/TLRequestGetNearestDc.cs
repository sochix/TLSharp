using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(531836966)]
    public class TLRequestGetNearestDc : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 531836966;
            }
        }

        public TLNearestDc Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLNearestDc)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }
    }
}
