using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-206066487)]
    public class TLInputGeoPoint : TLAbsInputGeoPoint
    {
        public override int Constructor
        {
            get
            {
                return -206066487;
            }
        }

        public double Lat { get; set; }
        public double Long { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Lat = br.ReadDouble();
            Long = br.ReadDouble();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Lat);
            bw.Write(Long);

        }
    }
}
