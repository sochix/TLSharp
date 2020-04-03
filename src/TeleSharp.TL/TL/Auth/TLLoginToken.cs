using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(1654593920)]
    public class TLLoginToken : TLAbsLoginToken
    {
        public override int Constructor
        {
            get
            {
                return 1654593920;
            }
        }

        public int Expires { get; set; }
        public byte[] Token { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Expires = br.ReadInt32();
            Token = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Expires);
            BytesUtil.Serialize(Token, bw);

        }
    }
}
