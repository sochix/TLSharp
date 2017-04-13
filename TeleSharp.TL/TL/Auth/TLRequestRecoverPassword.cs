using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(1319464594)]
    public class TLRequestRecoverPassword : TLMethod
    {
        public override int Constructor => 1319464594;

        public string code { get; set; }
        public TLAuthorization Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            code = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(code, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAuthorization) ObjectUtils.DeserializeObject(br);
        }
    }
}