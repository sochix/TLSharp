using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(1596029123)]
    public class TLRequestConfirmPhone : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1596029123;
            }
        }

        public string PhoneCode { get; set; }

        public string PhoneCodeHash { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhoneCodeHash = StringUtil.Deserialize(br);
            PhoneCode = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(PhoneCodeHash, bw);
            StringUtil.Serialize(PhoneCode, bw);
        }
    }
}
