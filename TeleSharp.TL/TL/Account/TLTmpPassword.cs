using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-614138572)]
    public class TLTmpPassword : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -614138572;
            }
        }

        public byte[] TmpPassword { get; set; }
        public int ValidUntil { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            TmpPassword = BytesUtil.Deserialize(br);
            ValidUntil = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(TmpPassword, bw);
            bw.Write(ValidUntil);

        }
    }
}
