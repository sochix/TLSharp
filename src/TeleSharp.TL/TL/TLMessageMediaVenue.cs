using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(2031269663)]
    public class TLMessageMediaVenue : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 2031269663;
            }
        }

        public TLAbsGeoPoint Geo { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Provider { get; set; }
        public string VenueId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            Title = StringUtil.Deserialize(br);
            Address = StringUtil.Deserialize(br);
            Provider = StringUtil.Deserialize(br);
            VenueId = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Geo, bw);
            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(Address, bw);
            StringUtil.Serialize(Provider, bw);
            StringUtil.Serialize(VenueId, bw);

        }
    }
}
