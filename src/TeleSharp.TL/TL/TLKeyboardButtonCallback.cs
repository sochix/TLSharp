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

        public string Text { get; set; }
        public byte[] Data { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = StringUtil.Deserialize(br);
            Data = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Text, bw);
            BytesUtil.Serialize(Data, bw);

        }
    }
}
