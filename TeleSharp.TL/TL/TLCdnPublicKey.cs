using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-914167110)]
    public class TLCdnPublicKey : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -914167110;
            }
        }

        public int DcId { get; set; }
        public string PublicKey { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            DcId = br.ReadInt32();
            PublicKey = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(DcId);
            StringUtil.Serialize(PublicKey, bw);

        }
    }
}
