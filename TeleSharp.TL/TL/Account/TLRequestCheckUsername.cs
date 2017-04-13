using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(655677548)]
    public class TLRequestCheckUsername : TLMethod
    {
        public override int Constructor => 655677548;

        public string username { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            username = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(username, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}