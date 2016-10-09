using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-644787419)]
    public class TLInputPhotoCrop : TLAbsInputPhotoCrop
    {
        public override int Constructor
        {
            get
            {
                return -644787419;
            }
        }

        public double crop_left { get; set; }
        public double crop_top { get; set; }
        public double crop_width { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            crop_left = br.ReadDouble();
            crop_top = br.ReadDouble();
            crop_width = br.ReadDouble();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(crop_left);
            bw.Write(crop_top);
            bw.Write(crop_width);

        }
    }
}
