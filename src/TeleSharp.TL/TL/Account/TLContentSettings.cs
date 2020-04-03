using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1474462241)]
    public class TLContentSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1474462241;
            }
        }

        public int Flags { get; set; }
        public bool SensitiveEnabled { get; set; }
        public bool SensitiveCanChange { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            SensitiveEnabled = (Flags & 1) != 0;
            SensitiveCanChange = (Flags & 2) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



        }
    }
}
