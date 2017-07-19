using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(1567990072)]
    public class TLDifferenceEmpty : TLAbsDifference
    {
        public override int Constructor
        {
            get
            {
                return 1567990072;
            }
        }

        public int date { get; set; }
        public int seq { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            date = br.ReadInt32();
            seq = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(date);
            bw.Write(seq);

        }
    }
}
