using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(608323678)]
    public class TLRequestSetAccountTTL : TLMethod
    {
        public override int Constructor => 608323678;

        public TLAccountDaysTTL ttl { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ttl = (TLAccountDaysTTL) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(ttl, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}