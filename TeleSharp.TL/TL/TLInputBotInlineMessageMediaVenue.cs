using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1431327288)]
    public class TLInputBotInlineMessageMediaVenue : TLAbsInputBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return -1431327288;
            }
        }

        public int flags { get; set; }
        public TLAbsInputGeoPoint geo_point { get; set; }
        public string title { get; set; }
        public string address { get; set; }
        public string provider { get; set; }
        public string venue_id { get; set; }
        public TLAbsReplyMarkup reply_markup { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = reply_markup != null ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            geo_point = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);
            title = StringUtil.Deserialize(br);
            address = StringUtil.Deserialize(br);
            provider = StringUtil.Deserialize(br);
            venue_id = StringUtil.Deserialize(br);
            if ((flags & 4) != 0)
                reply_markup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                reply_markup = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            ObjectUtils.SerializeObject(geo_point, bw);
            StringUtil.Serialize(title, bw);
            StringUtil.Serialize(address, bw);
            StringUtil.Serialize(provider, bw);
            StringUtil.Serialize(venue_id, bw);
            if ((flags & 4) != 0)
                ObjectUtils.SerializeObject(reply_markup, bw);

        }
    }
}
