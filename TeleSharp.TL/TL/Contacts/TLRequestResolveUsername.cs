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

        public Contacts.TLResolvedPeer Response { get; set; }

        public string Username { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Username = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLResolvedPeer)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Username, bw);
        }
    }
}
