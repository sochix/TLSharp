using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(1877286395)]
    public class TLRequestCheckPhone : TLMethod
    {
        public override int Constructor => 1877286395;

        public string phone_number { get; set; }
        public TLCheckedPhone Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(phone_number, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLCheckedPhone) ObjectUtils.DeserializeObject(br);
        }
    }
}