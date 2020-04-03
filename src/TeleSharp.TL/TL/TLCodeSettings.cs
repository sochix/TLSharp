using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-557924733)]
    public class TLCodeSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -557924733;
            }
        }

        public int Flags { get; set; }
        public bool AllowFlashcall { get; set; }
        public bool CurrentNumber { get; set; }
        public bool AllowAppHash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            AllowFlashcall = (Flags & 1) != 0;
            CurrentNumber = (Flags & 2) != 0;
            AllowAppHash = (Flags & 16) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);




        }
    }
}
