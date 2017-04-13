using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(1998331287)]
    public class TLRequestSendInvites : TLMethod
    {
        public override int Constructor => 1998331287;

        public TLVector<string> phone_numbers { get; set; }
        public string message { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_numbers = ObjectUtils.DeserializeVector<string>(br);
            message = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(phone_numbers, bw);
            StringUtil.Serialize(message, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}