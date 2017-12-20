using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-400399203)]
    public class TLRequestGetGameHighScores : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -400399203;
            }
        }

        public int Id { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public Messages.TLHighScores Response { get; set; }

        public TLAbsInputUser UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Id = br.ReadInt32();
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLHighScores)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(Id);
            ObjectUtils.SerializeObject(UserId, bw);
        }
    }
}
