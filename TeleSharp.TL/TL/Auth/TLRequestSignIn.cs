using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(-1126886015)]
    public class TLRequestSignIn : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1126886015;
            }
        }

        public string PhoneCode { get; set; }

        public string PhoneCodeHash { get; set; }

        public string PhoneNumber { get; set; }

        public Auth.TLAuthorization Response { get; set; }

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
            Response = (Auth.TLAuthorization)ObjectUtils.DeserializeObject(br);
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
