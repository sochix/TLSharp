using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-92517498)]
    public class TLRequestUpdatePasswordSettings : TLMethod
    {
        public override int Constructor => -92517498;

        public byte[] current_password_hash { get; set; }
        public TLPasswordInputSettings new_settings { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            current_password_hash = BytesUtil.Deserialize(br);
            new_settings = (TLPasswordInputSettings) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(current_password_hash, bw);
            ObjectUtils.SerializeObject(new_settings, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}