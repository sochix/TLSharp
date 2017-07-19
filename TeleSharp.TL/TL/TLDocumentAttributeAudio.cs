using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1739392570)]
    public class TLDocumentAttributeAudio : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return -1739392570;
            }
        }

        public int flags { get; set; }
        public bool voice { get; set; }
        public int duration { get; set; }
        public string title { get; set; }
        public string performer { get; set; }
        public byte[] waveform { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = voice ? (flags | 1024) : (flags & ~1024);
            flags = title != null ? (flags | 1) : (flags & ~1);
            flags = performer != null ? (flags | 2) : (flags & ~2);
            flags = waveform != null ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            voice = (flags & 1024) != 0;
            duration = br.ReadInt32();
            if ((flags & 1) != 0)
                title = StringUtil.Deserialize(br);
            else
                title = null;

            if ((flags & 2) != 0)
                performer = StringUtil.Deserialize(br);
            else
                performer = null;

            if ((flags & 4) != 0)
                waveform = BytesUtil.Deserialize(br);
            else
                waveform = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(duration);
            if ((flags & 1) != 0)
                StringUtil.Serialize(title, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(performer, bw);
            if ((flags & 4) != 0)
                BytesUtil.Serialize(waveform, bw);

        }
    }
}
