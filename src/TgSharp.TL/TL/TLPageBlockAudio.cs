using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-2143067670)]
    public class TLPageBlockAudio : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -2143067670;
            }
        }

        public long AudioId { get; set; }
        public TLPageCaption Caption { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            AudioId = br.ReadInt64();
            Caption = (TLPageCaption)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(AudioId);
            ObjectUtils.SerializeObject(Caption, bw);
        }
    }
}
