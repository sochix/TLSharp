using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1160515173)]
    public class TLMessage : TLAbsMessage
    {
        public override int Constructor
        {
            get
            {
                return 1160515173;
            }
        }

        public int Flags { get; set; }
        public bool Out { get; set; }
        public bool Mentioned { get; set; }
        public bool MediaUnread { get; set; }
        public bool Silent { get; set; }
        public bool Post { get; set; }
        public bool FromScheduled { get; set; }
        public bool Legacy { get; set; }
        public bool EditHide { get; set; }
        public int Id { get; set; }
        public int? FromId { get; set; }
        public TLAbsPeer ToId { get; set; }
        public TLMessageFwdHeader FwdFrom { get; set; }
        public int? ViaBotId { get; set; }
        public int? ReplyToMsgId { get; set; }
        public int Date { get; set; }
        public string Message { get; set; }
        public TLAbsMessageMedia Media { get; set; }
        public TLAbsReplyMarkup ReplyMarkup { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public int? Views { get; set; }
        public int? EditDate { get; set; }
        public string PostAuthor { get; set; }
        public long? GroupedId { get; set; }
        public TLVector<TLRestrictionReason> RestrictionReason { get; set; }


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
            Post = (Flags & 16384) != 0;
            FromScheduled = (Flags & 262144) != 0;
            Legacy = (Flags & 524288) != 0;
            EditHide = (Flags & 2097152) != 0;
            Id = br.ReadInt32();
            if ((Flags & 256) != 0)
                FromId = br.ReadInt32();
            else
                FromId = null;

            ToId = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
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

            Date = br.ReadInt32();
            Message = StringUtil.Deserialize(br);
            if ((Flags & 512) != 0)
                Media = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
            else
                Media = null;

            if ((Flags & 64) != 0)
                ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                ReplyMarkup = null;

            if ((Flags & 128) != 0)
                Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                Entities = null;

            if ((Flags & 1024) != 0)
                Views = br.ReadInt32();
            else
                Views = null;

            if ((Flags & 32768) != 0)
                EditDate = br.ReadInt32();
            else
                EditDate = null;

            if ((Flags & 65536) != 0)
                PostAuthor = StringUtil.Deserialize(br);
            else
                PostAuthor = null;

            if ((Flags & 131072) != 0)
                GroupedId = br.ReadInt64();
            else
                GroupedId = null;

            if ((Flags & 4194304) != 0)
                RestrictionReason = (TLVector<TLRestrictionReason>)ObjectUtils.DeserializeVector<TLRestrictionReason>(br);
            else
                RestrictionReason = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);








            bw.Write(Id);
            if ((Flags & 256) != 0)
                bw.Write(FromId.Value);
            ObjectUtils.SerializeObject(ToId, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(FwdFrom, bw);
            if ((Flags & 2048) != 0)
                bw.Write(ViaBotId.Value);
            if ((Flags & 8) != 0)
                bw.Write(ReplyToMsgId.Value);
            bw.Write(Date);
            StringUtil.Serialize(Message, bw);
            if ((Flags & 512) != 0)
                ObjectUtils.SerializeObject(Media, bw);
            if ((Flags & 64) != 0)
                ObjectUtils.SerializeObject(ReplyMarkup, bw);
            if ((Flags & 128) != 0)
                ObjectUtils.SerializeObject(Entities, bw);
            if ((Flags & 1024) != 0)
                bw.Write(Views.Value);
            if ((Flags & 32768) != 0)
                bw.Write(EditDate.Value);
            if ((Flags & 65536) != 0)
                StringUtil.Serialize(PostAuthor, bw);
            if ((Flags & 131072) != 0)
                bw.Write(GroupedId.Value);
            if ((Flags & 4194304) != 0)
                ObjectUtils.SerializeObject(RestrictionReason, bw);

        }
    }
}
