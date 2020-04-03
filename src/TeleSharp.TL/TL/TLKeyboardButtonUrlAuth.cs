using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(280464681)]
    public class TLKeyboardButtonUrlAuth : TLAbsKeyboardButton
    {
        public override int Constructor
        {
            get
            {
                return 280464681;
            }
        }

        public int Flags { get; set; }
        public string Text { get; set; }
        public string FwdText { get; set; }
        public string Url { get; set; }
        public int ButtonId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Text = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                FwdText = StringUtil.Deserialize(br);
            else
                FwdText = null;

            Url = StringUtil.Deserialize(br);
            ButtonId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            StringUtil.Serialize(Text, bw);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(FwdText, bw);
            StringUtil.Serialize(Url, bw);
            bw.Write(ButtonId);

        }
    }
}
