using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(250621158)]
    public class TLDocumentAttributeVideo : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return 250621158;
            }
        }

        public int flags { get; set; }
        public bool round_message { get; set; }
        public int duration { get; set; }
        public int w { get; set; }
        public int h { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = round_message ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            round_message = (flags & 1) != 0;
            duration = br.ReadInt32();
            w = br.ReadInt32();
            h = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(duration);
            bw.Write(w);
            bw.Write(h);

        }
    }
}
