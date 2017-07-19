using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-640214938)]
    public class TLPageBlockVideo : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -640214938;
            }
        }

        public int flags { get; set; }
        public bool autoplay { get; set; }
        public bool loop { get; set; }
        public long video_id { get; set; }
        public TLAbsRichText caption { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = autoplay ? (flags | 1) : (flags & ~1);
            flags = loop ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            autoplay = (flags & 1) != 0;
            loop = (flags & 2) != 0;
            video_id = br.ReadInt64();
            caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            bw.Write(video_id);
            ObjectUtils.SerializeObject(caption, bw);

        }
    }
}
