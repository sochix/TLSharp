using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1059185703)]
    public class TLJsonObjectValue : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1059185703;
            }
        }

        public string Key { get; set; }
        public TLAbsJSONValue Value { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Key = StringUtil.Deserialize(br);
            Value = (TLAbsJSONValue)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Key, bw);
            ObjectUtils.SerializeObject(Value, bw);

        }
    }
}
