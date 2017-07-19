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

        public int flags { get; set; }
        public bool same_peer { get; set; }
        public string text { get; set; }
        public string query { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = same_peer ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            same_peer = (flags & 1) != 0;
            text = StringUtil.Deserialize(br);
            query = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            StringUtil.Serialize(text, bw);
            StringUtil.Serialize(query, bw);

        }
    }
}
