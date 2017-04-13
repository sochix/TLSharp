using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-113456221)]
    public class TLRequestResolveUsername : TLMethod
    {
        public override int Constructor => -113456221;

        public string username { get; set; }
        public TLResolvedPeer Response { get; set; }


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
            Response = (TLResolvedPeer) ObjectUtils.DeserializeObject(br);
        }
    }
}