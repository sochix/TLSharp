using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(90744648)]
    public class TLKeyboardButtonSwitchInline : TLAbsKeyboardButton
    {
        public override int Constructor
        {
            get
            {
                return 90744648;
            }
        }

        public int Flags { get; set; }
        public bool SamePeer { get; set; }
        public string Text { get; set; }
        public string Query { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = SamePeer ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            SamePeer = (Flags & 1) != 0;
            Text = StringUtil.Deserialize(br);
            Query = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            StringUtil.Serialize(Text, bw);
            StringUtil.Serialize(Query, bw);

        }
    }
}
