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

        public int Flags { get; set; }
        public bool SingleUse { get; set; }
        public bool Selective { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = SingleUse ? (Flags | 2) : (Flags & ~2);
            Flags = Selective ? (Flags | 4) : (Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            SingleUse = (Flags & 2) != 0;
            Selective = (Flags & 4) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);



        }
    }
}
