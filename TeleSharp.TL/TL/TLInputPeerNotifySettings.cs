using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(949182130)]
    public class TLInputPeerNotifySettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 949182130;
            }
        }

        public int flags { get; set; }
        public bool show_previews { get; set; }
        public bool silent { get; set; }
        public int mute_until { get; set; }
        public string sound { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = show_previews ? (flags | 1) : (flags & ~1);
            flags = silent ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            show_previews = (flags & 1) != 0;
            silent = (flags & 2) != 0;
            mute_until = br.ReadInt32();
            sound = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            bw.Write(mute_until);
            StringUtil.Serialize(sound, bw);

        }
    }
}
