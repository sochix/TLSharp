using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(174260510)]
    public class TLRequestCheckPassword : TLMethod
    {
        public override int Constructor => 174260510;

        public byte[] password_hash { get; set; }
        public TLAuthorization Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            password_hash = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(password_hash, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAuthorization) ObjectUtils.DeserializeObject(br);
        }
    }
}