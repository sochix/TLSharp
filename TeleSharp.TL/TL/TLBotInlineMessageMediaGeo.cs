using System.IO;

namespace TeleSharp.TL
{
    [TLObject(982505656)]
    public class TLBotInlineMessageMediaGeo : TLAbsBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return 982505656;
            }
        }

        public int Flags { get; set; }

        public TLAbsGeoPoint Geo { get; set; }

        public TLAbsReplyMarkup ReplyMarkup { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            if ((Flags & 4) != 0)
                ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                ReplyMarkup = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Geo, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ReplyMarkup, bw);
        }
    }
}
