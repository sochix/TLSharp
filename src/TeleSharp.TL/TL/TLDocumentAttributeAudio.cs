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

        public int Flags { get; set; }
        public bool Voice { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public string Performer { get; set; }
        public byte[] Waveform { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Voice ? (Flags | 1024) : (Flags & ~1024);
            Flags = Title != null ? (Flags | 1) : (Flags & ~1);
            Flags = Performer != null ? (Flags | 2) : (Flags & ~2);
            Flags = Waveform != null ? (Flags | 4) : (Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Voice = (Flags & 1024) != 0;
            Duration = br.ReadInt32();
            if ((Flags & 1) != 0)
                Title = StringUtil.Deserialize(br);
            else
                Title = null;

            if ((Flags & 2) != 0)
                Performer = StringUtil.Deserialize(br);
            else
                Performer = null;

            if ((Flags & 4) != 0)
                Waveform = BytesUtil.Deserialize(br);
            else
                Waveform = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            bw.Write(Duration);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Title, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(Performer, bw);
            if ((Flags & 4) != 0)
                BytesUtil.Serialize(Waveform, bw);

        }
    }
}
