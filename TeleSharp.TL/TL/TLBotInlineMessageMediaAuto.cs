using System.IO;

namespace TeleSharp.TL
{
    [TLObject(175419739)]
    public class TLBotInlineMessageMediaAuto : TLAbsBotInlineMessage
    {
        public string Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return 175419739;
            }
        }

        public int Flags { get; set; }

        public TLAbsReplyMarkup ReplyMarkup { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Caption = StringUtil.Deserialize(br);
            if ((Flags & 4) != 0)
                ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                ReplyMarkup = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            StringUtil.Serialize(Caption, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ReplyMarkup, bw);
        }
    }
}
