using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(43446532)]
    public class TLGeoPoint : TLAbsGeoPoint
    {
        public override int Constructor
        {
            get
            {
                return 43446532;
            }
        }

        public double Long { get; set; }
        public double Lat { get; set; }
        public long AccessHash { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Long = br.ReadDouble();
            Lat = br.ReadDouble();
            AccessHash = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Long);
            bw.Write(Lat);
            bw.Write(AccessHash);
        }
    }
}
