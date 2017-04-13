using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-1131605573)]
    public class TLRequestGetPasswordSettings : TLMethod
    {
        public override int Constructor => -1131605573;

        public byte[] current_password_hash { get; set; }
        public TLPasswordSettings Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            current_password_hash = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(current_password_hash, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLPasswordSettings) ObjectUtils.DeserializeObject(br);
        }
    }
}