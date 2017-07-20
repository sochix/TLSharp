using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1269012015)]
    public class TLAffectedHistory : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1269012015;
            }
        }

        public int pts { get; set; }
        public int pts_count { get; set; }
        public int offset { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            pts = br.ReadInt32();
            pts_count = br.ReadInt32();
            offset = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(pts);
            bw.Write(pts_count);
            bw.Write(offset);

        }
    }
}
