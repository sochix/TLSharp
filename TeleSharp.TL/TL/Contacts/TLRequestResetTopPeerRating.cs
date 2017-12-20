using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(451113900)]
    public class TLRequestResetTopPeerRating : TLMethod
    {
        public TLAbsTopPeerCategory Category { get; set; }

        public override int Constructor
        {
            get
            {
                return 451113900;
            }
        }

        public TLAbsInputPeer Peer { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Category = (TLAbsTopPeerCategory)ObjectUtils.DeserializeObject(br);
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Category, bw);
            ObjectUtils.SerializeObject(Peer, bw);
        }
    }
}
