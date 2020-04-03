using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(110008598)]
    public class TLLoginTokenMigrateTo : TLAbsLoginToken
    {
        public override int Constructor
        {
            get
            {
                return 110008598;
            }
        }

        public int DcId { get; set; }
        public byte[] Token { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            DcId = br.ReadInt32();
            Token = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(DcId);
            BytesUtil.Serialize(Token, bw);

        }
    }
}
