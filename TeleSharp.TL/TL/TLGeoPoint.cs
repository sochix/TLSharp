using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(541710092)]
    public class TLGeoPoint : TLAbsGeoPoint
    {
        public override int Constructor
        {
            get
            {
                return 541710092;
            }
        }

        public double @long { get; set; }
        public double lat { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            @long = br.ReadDouble();
            lat = br.ReadDouble();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(@long);
            bw.Write(lat);

        }
    }
}
