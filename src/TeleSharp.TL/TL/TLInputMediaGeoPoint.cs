using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-104578748)]
    public class TLInputMediaGeoPoint : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -104578748;
            }
        }

        public TLAbsInputGeoPoint GeoPoint { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            GeoPoint = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(GeoPoint, bw);

        }
    }
}
