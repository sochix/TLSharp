using System.IO;

namespace TeleSharp.TL
{
    [TLObject(904770772)]
    public class TLBotInlineMessageMediaContact : TLAbsBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return 904770772;
            }
        }

        public string FirstName { get; set; }

        public int Flags { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public TLAbsReplyMarkup ReplyMarkup { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            PhoneNumber = StringUtil.Deserialize(br);
            FirstName = StringUtil.Deserialize(br);
            LastName = StringUtil.Deserialize(br);
            if ((Flags & 4) != 0)
                ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                ReplyMarkup = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            StringUtil.Serialize(PhoneNumber, bw);
            StringUtil.Serialize(FirstName, bw);
            StringUtil.Serialize(LastName, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ReplyMarkup, bw);
        }
    }
}
