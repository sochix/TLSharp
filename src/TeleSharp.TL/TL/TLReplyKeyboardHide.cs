using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1606526075)]
    public class TLReplyKeyboardHide : TLAbsReplyMarkup
    {
        public override int Constructor
        {
            get
            {
                return -1606526075;
            }
        }

        public int Flags { get; set; }
        public bool Selective { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Selective = (Flags & 4) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


        }
    }
}
