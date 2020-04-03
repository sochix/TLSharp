using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1964327229)]
    public class TLSecureData : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1964327229;
            }
        }

        public byte[] Data { get; set; }
        public byte[] DataHash { get; set; }
        public byte[] Secret { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Data = BytesUtil.Deserialize(br);
            DataHash = BytesUtil.Deserialize(br);
            Secret = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(Data, bw);
            BytesUtil.Serialize(DataHash, bw);
            BytesUtil.Serialize(Secret, bw);

        }
    }
}
