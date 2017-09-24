using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-113456221)]
    public class TLRequestResolveUsername : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -113456221;
            }
        }

        public string username { get; set; }
        public Contacts.TLResolvedPeer Response { get; set; }

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
            Response = (Contacts.TLResolvedPeer)ObjectUtils.DeserializeObject(br);
        }
    }
}