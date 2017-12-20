using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1896289088)]
    public class TLRequestSetGameScore : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1896289088;
            }
        }

        public bool EditMessage { get; set; }

        public int Flags { get; set; }

        public bool Force { get; set; }

        public int Id { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public TLAbsUpdates Response { get; set; }

        public int Score { get; set; }

        public TLAbsInputUser UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            EditMessage = (Flags & 1) != 0;
            Force = (Flags & 2) != 0;
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Id = br.ReadInt32();
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            Score = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(Id);
            ObjectUtils.SerializeObject(UserId, bw);
            bw.Write(Score);
        }
    }
}
