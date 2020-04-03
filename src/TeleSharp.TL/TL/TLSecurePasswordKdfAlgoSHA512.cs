using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2042159726)]
    public class TLSecurePasswordKdfAlgoSHA512 : TLAbsSecurePasswordKdfAlgo
    {
        public override int Constructor
        {
            get
            {
                return -2042159726;
            }
        }

        public byte[] Salt { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Salt = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(Salt, bw);

        }
    }
}
