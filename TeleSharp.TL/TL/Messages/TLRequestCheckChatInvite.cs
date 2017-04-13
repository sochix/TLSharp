using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1051570619)]
    public class TLRequestCheckChatInvite : TLMethod
    {
        public override int Constructor => 1051570619;

        public string hash { get; set; }
        public TLAbsChatInvite Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(hash, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsChatInvite) ObjectUtils.DeserializeObject(br);
        }
    }
}