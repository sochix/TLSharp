using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(547062491)]
    public class TLChannelLocation : TLAbsChannelLocation
    {
        public override int Constructor
        {
            get
            {
                return 547062491;
            }
        }

        public TLAbsGeoPoint GeoPoint { get; set; }
        public string Address { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            GeoPoint = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            Address = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(GeoPoint, bw);
            StringUtil.Serialize(Address, bw);

        }
    }
}
