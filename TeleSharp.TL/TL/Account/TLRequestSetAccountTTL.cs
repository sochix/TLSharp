using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(608323678)]
    public class TLRequestSetAccountTTL : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 608323678;
            }
        }

        public bool Response { get; set; }

        public TLAccountDaysTTL Ttl { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Ttl = (TLAccountDaysTTL)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Ttl, bw);
        }
    }
}
