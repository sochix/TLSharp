using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-200242528)]
    public class TLReplyKeyboardForceReply : TLAbsReplyMarkup
    {
        public override int Constructor
        {
            get
            {
                return -200242528;
            }
        }

        public int flags { get; set; }
        public bool single_use { get; set; }
        public bool selective { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = single_use ? (flags | 2) : (flags & ~2);
            flags = selective ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            single_use = (flags & 2) != 0;
            selective = (flags & 4) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);



        }
    }
}
