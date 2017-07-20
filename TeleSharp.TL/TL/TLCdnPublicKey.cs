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

        public int dc_id { get; set; }
        public string public_key { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            dc_id = br.ReadInt32();
            public_key = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(dc_id);
            StringUtil.Serialize(public_key, bw);

        }
    }
}
