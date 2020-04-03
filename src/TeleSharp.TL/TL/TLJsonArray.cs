using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-146520221)]
    public class TLJsonArray : TLAbsJSONValue
    {
        public override int Constructor
        {
            get
            {
                return -146520221;
            }
        }

        public TLVector<TLAbsJSONValue> Value { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Value = (TLVector<TLAbsJSONValue>)ObjectUtils.DeserializeVector<TLAbsJSONValue>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Value, bw);

        }
    }
}
