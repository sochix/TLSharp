using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(982592842)]
    public class TLPasswordKdfAlgoSHA256SHA256PBKDF2HMACSHA512iter100000SHA256ModPow : TLAbsPasswordKdfAlgo
    {
        public override int Constructor
        {
            get
            {
                return 982592842;
            }
        }

        public byte[] Salt1 { get; set; }
        public byte[] Salt2 { get; set; }
        public int G { get; set; }
        public byte[] P { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Salt1 = BytesUtil.Deserialize(br);
            Salt2 = BytesUtil.Deserialize(br);
            G = br.ReadInt32();
            P = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(Salt1, bw);
            BytesUtil.Serialize(Salt2, bw);
            bw.Write(G);
            BytesUtil.Serialize(P, bw);

        }
    }
}
