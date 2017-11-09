using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(-1519637954)]
    public class TLState : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1519637954;
            }
        }

        public int Pts { get; set; }
        public int Qts { get; set; }
        public int Date { get; set; }
        public int Seq { get; set; }
        public int UnreadCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Pts = br.ReadInt32();
            Qts = br.ReadInt32();
            Date = br.ReadInt32();
            Seq = br.ReadInt32();
            UnreadCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Pts);
            bw.Write(Qts);
            bw.Write(Date);
            bw.Write(Seq);
            bw.Write(UnreadCount);

        }
    }
}
