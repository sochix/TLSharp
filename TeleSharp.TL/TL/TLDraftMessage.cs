using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-40996577)]
    public class TLDraftMessage : TLAbsDraftMessage
    {
        public override int Constructor
        {
            get
            {
                return -40996577;
            }
        }

        public int Date { get; set; }

        public TLVector<TLAbsMessageEntity> Entities { get; set; }

        public int Flags { get; set; }

        public string Message { get; set; }

        public bool NoWebpage { get; set; }

        public int? ReplyToMsgId { get; set; }

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

            Message = StringUtil.Deserialize(br);
            if ((Flags & 8) != 0)
                Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                Entities = null;

            Date = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            if ((Flags & 1) != 0)
                bw.Write(ReplyToMsgId.Value);
            StringUtil.Serialize(Message, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(Entities, bw);
            bw.Write(Date);
        }
    }
}
