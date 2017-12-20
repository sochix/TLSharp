using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1130767150)]
    public class TLBotInlineMessageMediaVenue : TLAbsBotInlineMessage
    {
        public string Address { get; set; }

        public override int Constructor
        {
            get
            {
                return 1130767150;
            }
        }

        public int Flags { get; set; }

        public TLAbsGeoPoint Geo { get; set; }

        public string Provider { get; set; }

        public TLAbsReplyMarkup ReplyMarkup { get; set; }

        public string Title { get; set; }

        public string VenueId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            Title = StringUtil.Deserialize(br);
            Address = StringUtil.Deserialize(br);
            Provider = StringUtil.Deserialize(br);
            VenueId = StringUtil.Deserialize(br);
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
            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(Address, bw);
            StringUtil.Serialize(Provider, bw);
            StringUtil.Serialize(VenueId, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ReplyMarkup, bw);
        }
    }
}
