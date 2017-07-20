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

        public byte[] tmp_password { get; set; }
        public int valid_until { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            tmp_password = BytesUtil.Deserialize(br);
            valid_until = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(tmp_password, bw);
            bw.Write(valid_until);

        }
    }
}
