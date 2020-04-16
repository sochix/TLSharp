using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-316748368)]
    public class TLSecureValueHash : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -316748368;
            }
        }

        public TLAbsSecureValueType Type { get; set; }
        public byte[] Hash { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Type = (TLAbsSecureValueType)ObjectUtils.DeserializeObject(br);
            Hash = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Type, bw);
            BytesUtil.Serialize(Hash, bw);
        }
    }
}
