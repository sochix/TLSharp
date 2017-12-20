using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1137057461)]
    public class TLRequestSaveDraft : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1137057461;
            }
        }

        public TLVector<TLAbsMessageEntity> Entities { get; set; }

        public int Flags { get; set; }

        public string Message { get; set; }

        public bool NoWebpage { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public int? ReplyToMsgId { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            NoWebpage = (Flags & 2) != 0;
            if ((Flags & 1) != 0)
                ReplyToMsgId = br.ReadInt32();
            else
                ReplyToMsgId = null;

            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Message = StringUtil.Deserialize(br);
            if ((Flags & 8) != 0)
                Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                Entities = null;
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            if ((Flags & 1) != 0)
                bw.Write(ReplyToMsgId.Value);
            ObjectUtils.SerializeObject(Peer, bw);
            StringUtil.Serialize(Message, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(Entities, bw);
        }
    }
}
