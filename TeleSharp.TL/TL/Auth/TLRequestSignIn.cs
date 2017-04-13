using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(-1126886015)]
    public class TLRequestSignIn : TLMethod
    {
        public override int Constructor => -1126886015;

        public string phone_number { get; set; }
        public string phone_code_hash { get; set; }
        public string phone_code { get; set; }
        public TLAuthorization Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);
            phone_code_hash = StringUtil.Deserialize(br);
            phone_code = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(phone_number, bw);
            StringUtil.Serialize(phone_code_hash, bw);
            StringUtil.Serialize(phone_code, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAuthorization) ObjectUtils.DeserializeObject(br);
        }
    }
}