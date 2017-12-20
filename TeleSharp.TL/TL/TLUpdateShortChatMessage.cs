using System.IO;

namespace TeleSharp.TL
{
    [TLObject(377562760)]
    public class TLUpdateShortChatMessage : TLAbsUpdates
    {
        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return 377562760;
            }
        }

        public int Date { get; set; }

        public TLVector<TLAbsMessageEntity> Entities { get; set; }

        public int Flags { get; set; }

        public int FromId { get; set; }

        public TLMessageFwdHeader FwdFrom { get; set; }

        public int Id { get; set; }

        public bool MediaUnread { get; set; }

        public bool Mentioned { get; set; }

        public string Message { get; set; }

        public bool Out { get; set; }

        public int Pts { get; set; }

        public int PtsCount { get; set; }

        public int? ReplyToMsgId { get; set; }

        public bool Silent { get; set; }

        public int? ViaBotId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Out = (Flags & 2) != 0;
            Mentioned = (Flags & 16) != 0;
            MediaUnread = (Flags & 32) != 0;
            Silent = (Flags & 8192) != 0;
            Id = br.ReadInt32();
            FromId = br.ReadInt32();
            ChatId = br.ReadInt32();
            Message = StringUtil.Deserialize(br);
            Pts = br.ReadInt32();
            PtsCount = br.ReadInt32();
            Date = br.ReadInt32();
            if ((Flags & 4) != 0)
                FwdFrom = (TLMessageFwdHeader)ObjectUtils.DeserializeObject(br);
            else
                FwdFrom = null;

            if ((Flags & 2048) != 0)
                ViaBotId = br.ReadInt32();
            else
                ViaBotId = null;

            if ((Flags & 8) != 0)
                ReplyToMsgId = br.ReadInt32();
            else
                ReplyToMsgId = null;

            if ((Flags & 128) != 0)
                Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                Entities = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);




            bw.Write(Id);
            bw.Write(FromId);
            bw.Write(ChatId);
            StringUtil.Serialize(Message, bw);
            bw.Write(Pts);
            bw.Write(PtsCount);
            bw.Write(Date);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(FwdFrom, bw);
            if ((Flags & 2048) != 0)
                bw.Write(ViaBotId.Value);
            if ((Flags & 8) != 0)
                bw.Write(ReplyToMsgId.Value);
            if ((Flags & 128) != 0)
                ObjectUtils.SerializeObject(Entities, bw);
        }
    }
}
