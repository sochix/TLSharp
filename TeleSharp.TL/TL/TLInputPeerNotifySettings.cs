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

        public int Flags { get; set; }
        public bool ShowPreviews { get; set; }
        public bool Silent { get; set; }
        public int MuteUntil { get; set; }
        public string Sound { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = ShowPreviews ? (Flags | 1) : (Flags & ~1);
            Flags = Silent ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ShowPreviews = (Flags & 1) != 0;
            Silent = (Flags & 2) != 0;
            MuteUntil = br.ReadInt32();
            Sound = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);


            bw.Write(MuteUntil);
            StringUtil.Serialize(Sound, bw);

        }
    }
}
