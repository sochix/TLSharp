using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1462101002)]
    public class TLCdnConfig : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1462101002;
            }
        }

        public TLVector<TLCdnPublicKey> public_keys { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            public_keys = (TLVector<TLCdnPublicKey>)ObjectUtils.DeserializeVector<TLCdnPublicKey>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(public_keys, bw);

        }
    }
}
