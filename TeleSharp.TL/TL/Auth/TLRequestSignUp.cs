using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(453408308)]
    public class TLRequestSignUp : TLMethod
    {
        public override int Constructor => 453408308;

        public string phone_number { get; set; }
        public string phone_code_hash { get; set; }
        public string phone_code { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public TLAuthorization Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);
            phone_code_hash = StringUtil.Deserialize(br);
            phone_code = StringUtil.Deserialize(br);
            first_name = StringUtil.Deserialize(br);
            last_name = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(phone_number, bw);
            StringUtil.Serialize(phone_code_hash, bw);
            StringUtil.Serialize(phone_code, bw);
            StringUtil.Serialize(first_name, bw);
            StringUtil.Serialize(last_name, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAuthorization) ObjectUtils.DeserializeObject(br);
        }
    }
}