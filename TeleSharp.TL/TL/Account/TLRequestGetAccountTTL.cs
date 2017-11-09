using System.IO;
namespace TeleSharp.TL.Account
{
    [TLObject(150761757)]
    public class TLRequestGetAccountTTL : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 150761757;
            }
        }

        public TLAccountDaysTTL Response { get; set; }


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
            Response = (TLAccountDaysTTL)ObjectUtils.DeserializeObject(br);

        }
    }
}
