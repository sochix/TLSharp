using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(2084316681)]
    public class TLMessageMediaGeoLive : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 2084316681;
            }
        }

        public TLAbsGeoPoint Geo { get; set; }
        public int Period { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            Period = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Geo, bw);
            bw.Write(Period);
        }
    }
}
