using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(319564933)]
    public class TLRequestEditInlineBotMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 319564933;
            }
        }

        public TLVector<TLAbsMessageEntity> Entities { get; set; }

        public int Flags { get; set; }

        public TLInputBotInlineMessageID Id { get; set; }

        public string Message { get; set; }

        public bool NoWebpage { get; set; }

        public TLAbsReplyMarkup ReplyMarkup { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            NoWebpage = (Flags & 2) != 0;
            Id = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
            if ((Flags & 2048) != 0)
                Message = StringUtil.Deserialize(br);
            else
                Message = null;

            if ((Flags & 4) != 0)
                ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                ReplyMarkup = null;

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

            ObjectUtils.SerializeObject(Id, bw);
            if ((Flags & 2048) != 0)
                StringUtil.Serialize(Message, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ReplyMarkup, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(Entities, bw);
        }
    }
}
