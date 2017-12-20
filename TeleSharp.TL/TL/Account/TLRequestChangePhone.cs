using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(1891839707)]
    public class TLRequestChangePhone : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1891839707;
            }
        }

        public string PhoneCode { get; set; }

        public string PhoneCodeHash { get; set; }

        public string PhoneNumber { get; set; }

        public TLAbsUser Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhoneNumber = StringUtil.Deserialize(br);
            PhoneCodeHash = StringUtil.Deserialize(br);
            PhoneCode = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUser)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(PhoneNumber, bw);
            StringUtil.Serialize(PhoneCodeHash, bw);
            StringUtil.Serialize(PhoneCode, bw);
        }
    }
}
