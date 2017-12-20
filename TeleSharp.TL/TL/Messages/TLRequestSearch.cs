using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(60726944)]
    public class TLRequestSearch : TLMethod
    {
        public int AddOffset { get; set; }

        public override int Constructor
        {
            get
            {
                return 60726944;
            }
        }

        public TLAbsMessagesFilter Filter { get; set; }

        public int Flags { get; set; }

        public TLAbsInputUser FromId { get; set; }

        public int Limit { get; set; }

        public int MaxDate { get; set; }

        public int MaxId { get; set; }

        public int MinDate { get; set; }

        public int MinId { get; set; }

        public int OffsetId { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public string Q { get; set; }

        public Messages.TLAbsMessages Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Q = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                FromId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            else
                FromId = null;

            Filter = (TLAbsMessagesFilter)ObjectUtils.DeserializeObject(br);
            MinDate = br.ReadInt32();
            MaxDate = br.ReadInt32();
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
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Peer, bw);
            StringUtil.Serialize(Q, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(FromId, bw);
            ObjectUtils.SerializeObject(Filter, bw);
            bw.Write(MinDate);
            bw.Write(MaxDate);
            bw.Write(OffsetId);
            bw.Write(AddOffset);
            bw.Write(Limit);
            bw.Write(MaxId);
            bw.Write(MinId);
        }
    }
}
