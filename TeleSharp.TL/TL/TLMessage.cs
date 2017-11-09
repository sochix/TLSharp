using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1063525281)]
    public class TLMessage : TLAbsMessage
    {
        public override int Constructor
        {
            get
            {
                return -1063525281;
            }
        }

        public int Flags { get; set; }
        public bool Out { get; set; }
        public bool Mentioned { get; set; }
        public bool MediaUnread { get; set; }
        public bool Silent { get; set; }
        public bool Post { get; set; }
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


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Out ? (Flags | 2) : (Flags & ~2);
            Flags = Mentioned ? (Flags | 16) : (Flags & ~16);
            Flags = MediaUnread ? (Flags | 32) : (Flags & ~32);
            Flags = Silent ? (Flags | 8192) : (Flags & ~8192);
            Flags = Post ? (Flags | 16384) : (Flags & ~16384);
            Flags = FromId != null ? (Flags | 256) : (Flags & ~256);
            Flags = FwdFrom != null ? (Flags | 4) : (Flags & ~4);
            Flags = ViaBotId != null ? (Flags | 2048) : (Flags & ~2048);
            Flags = ReplyToMsgId != null ? (Flags | 8) : (Flags & ~8);
            Flags = Media != null ? (Flags | 512) : (Flags & ~512);
            Flags = ReplyMarkup != null ? (Flags | 64) : (Flags & ~64);
            Flags = Entities != null ? (Flags | 128) : (Flags & ~128);
            Flags = Views != null ? (Flags | 1024) : (Flags & ~1024);
            Flags = EditDate != null ? (Flags | 32768) : (Flags & ~32768);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Out = (Flags & 2) != 0;
            Mentioned = (Flags & 16) != 0;
            MediaUnread = (Flags & 32) != 0;
            Silent = (Flags & 8192) != 0;
            Post = (Flags & 16384) != 0;
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


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
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

        }
    }
}
