using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1361650766)]
    public class TLMaskCoords : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1361650766;
            }
        }

        public int N { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Zoom { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            N = br.ReadInt32();
            X = br.ReadDouble();
            Y = br.ReadDouble();
            Zoom = br.ReadDouble();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(N);
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Zoom);

        }
    }
}
