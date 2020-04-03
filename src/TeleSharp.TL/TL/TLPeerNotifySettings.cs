using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1353671392)]
    public class TLPeerNotifySettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1353671392;
            }
        }

        public int Flags { get; set; }
        public bool? ShowPreviews { get; set; }
        public bool? Silent { get; set; }
        public int? MuteUntil { get; set; }
        public string Sound { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                ShowPreviews = BoolUtil.Deserialize(br);
            else
                ShowPreviews = null;

            if ((Flags & 2) != 0)
                Silent = BoolUtil.Deserialize(br);
            else
                Silent = null;

            if ((Flags & 4) != 0)
                MuteUntil = br.ReadInt32();
            else
                MuteUntil = null;

            if ((Flags & 8) != 0)
                Sound = StringUtil.Deserialize(br);
            else
                Sound = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                BoolUtil.Serialize(ShowPreviews.Value, bw);
            if ((Flags & 2) != 0)
                BoolUtil.Serialize(Silent.Value, bw);
            if ((Flags & 4) != 0)
                bw.Write(MuteUntil.Value);
            if ((Flags & 8) != 0)
                StringUtil.Serialize(Sound, bw);

        }
    }
}
