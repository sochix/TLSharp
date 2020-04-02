using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2122045747)]
    public class TLPeerSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -2122045747;
            }
        }

        public int Flags { get; set; }
        public bool ReportSpam { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = ReportSpam ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ReportSpam = (Flags & 1) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);


        }
    }
}
