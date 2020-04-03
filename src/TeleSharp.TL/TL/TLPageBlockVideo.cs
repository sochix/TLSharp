using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(2089805750)]
    public class TLPageBlockVideo : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 2089805750;
            }
        }

        public int Flags { get; set; }
        public bool Autoplay { get; set; }
        public bool Loop { get; set; }
        public long VideoId { get; set; }
        public TLPageCaption Caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Autoplay = (Flags & 1) != 0;
            Loop = (Flags & 2) != 0;
            VideoId = br.ReadInt64();
            Caption = (TLPageCaption)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            bw.Write(VideoId);
            ObjectUtils.SerializeObject(Caption, bw);

        }
    }
}
