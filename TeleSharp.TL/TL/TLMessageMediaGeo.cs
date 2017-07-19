using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1457575028)]
    public class TLMessageMediaGeo : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 1457575028;
            }
        }

        public TLAbsGeoPoint geo { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(geo, bw);

        }
    }
}
