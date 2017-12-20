using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1262639204)]
    public class TLInputBotInlineMessageGame : TLAbsInputBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return 1262639204;
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
            if ((Flags & 4) != 0)
                ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                ReplyMarkup = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ReplyMarkup, bw);
        }
    }
}
