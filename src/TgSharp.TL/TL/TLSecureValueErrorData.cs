using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-391902247)]
    public class TLSecureValueErrorData : TLAbsSecureValueError
    {
        public override int Constructor
        {
            get
            {
                return -391902247;
            }
        }

        public TLAbsSecureValueType Type { get; set; }
        public byte[] DataHash { get; set; }
        public string Field { get; set; }
        public string Text { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Type = (TLAbsSecureValueType)ObjectUtils.DeserializeObject(br);
            DataHash = BytesUtil.Deserialize(br);
            Field = StringUtil.Deserialize(br);
            Text = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Type, bw);
            BytesUtil.Serialize(DataHash, bw);
            StringUtil.Serialize(Field, bw);
            StringUtil.Serialize(Text, bw);
        }
    }
}
