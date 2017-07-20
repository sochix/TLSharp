using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1748655686)]
    public class TLKeyboardButtonCallback : TLAbsKeyboardButton
    {
        public override int Constructor
        {
            get
            {
                return 1748655686;
            }
        }

        public string text { get; set; }
        public byte[] data { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = StringUtil.Deserialize(br);
            data = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(text, bw);
            BytesUtil.Serialize(data, bw);

        }
    }
}
