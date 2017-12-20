using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1180140658)]
    public class TLRequestGetUnreadMentions : TLMethod
    {
        public int AddOffset { get; set; }

        public override int Constructor
        {
            get
            {
                return 1180140658;
            }
        }

        public int Limit { get; set; }

        public int MaxId { get; set; }

        public int MinId { get; set; }

        public int OffsetId { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public Messages.TLAbsMessages Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            OffsetId = br.ReadInt32();
            AddOffset = br.ReadInt32();
            Limit = br.ReadInt32();
            MaxId = br.ReadInt32();
            MinId = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(OffsetId);
            bw.Write(AddOffset);
            bw.Write(Limit);
            bw.Write(MaxId);
            bw.Write(MinId);
        }
    }
}
