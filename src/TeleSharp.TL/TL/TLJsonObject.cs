using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeleSharp.TL;

namespace TeleSharp.TL
{
    [TLObject(-1715350371)]
    public class TLJsonObject : TLAbsJSONValue
    {
        public override int Constructor
        {
            get
            {
                return -1715350371;
            }
        }

        public TLVector<TLJSONObjectValue> Value { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Value = (TLVector<TLJSONObjectValue>)ObjectUtils.DeserializeVector<TLJSONObjectValue>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Value, bw);
        }
    }
}
