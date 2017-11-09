using System.IO;
namespace TeleSharp.TL.Account
{
    [TLObject(-484392616)]
    public class TLRequestGetAuthorizations : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -484392616;
            }
        }

        public Account.TLAuthorizations Response { get; set; }


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
            Response = (Account.TLAuthorizations)ObjectUtils.DeserializeObject(br);

        }
    }
}
