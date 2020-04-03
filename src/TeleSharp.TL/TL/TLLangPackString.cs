using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-892239370)]
    public class TLLangPackString : TLAbsLangPackString
    {
        public override int Constructor
        {
            get
            {
                return -892239370;
            }
        }

        public string Key { get; set; }
        public string Value { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Key = StringUtil.Deserialize(br);
            Value = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Key, bw);
            StringUtil.Serialize(Value, bw);

        }
    }
}
