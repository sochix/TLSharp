using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-103646630)]
    public class TLUpdateInlineBotCallbackQuery : TLAbsUpdate
    {
        public long ChatInstance { get; set; }

        public override int Constructor
        {
            get
            {
                return -103646630;
            }
        }

        public byte[] Data { get; set; }

        public int Flags { get; set; }

        public string GameShortName { get; set; }

        public TLInputBotInlineMessageID MsgId { get; set; }

        public long QueryId { get; set; }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            QueryId = br.ReadInt64();
            UserId = br.ReadInt32();
            MsgId = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
            ChatInstance = br.ReadInt64();
            if ((Flags & 1) != 0)
                Data = BytesUtil.Deserialize(br);
            else
                Data = null;

            if ((Flags & 2) != 0)
                GameShortName = StringUtil.Deserialize(br);
            else
                GameShortName = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(QueryId);
            bw.Write(UserId);
            ObjectUtils.SerializeObject(MsgId, bw);
            bw.Write(ChatInstance);
            if ((Flags & 1) != 0)
                BytesUtil.Serialize(Data, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(GameShortName, bw);
        }
    }
}
