using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2134272152)]
    public class TLInputMessagesFilterPhoneCalls : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return -2134272152;
            }
        }

        public int flags { get; set; }
        public bool missed { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = missed ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            missed = (flags & 1) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


        }
    }
}
