using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-190472735)]
    public class TLInputBotInlineMessageMediaGeo : TLAbsInputBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return -190472735;
            }
        }

        public int Flags { get; set; }

        public TLAbsInputGeoPoint GeoPoint { get; set; }

        public TLAbsReplyMarkup ReplyMarkup { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            GeoPoint = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);
            if ((Flags & 4) != 0)
                ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                ReplyMarkup = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(GeoPoint, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ReplyMarkup, bw);
        }
    }
}
