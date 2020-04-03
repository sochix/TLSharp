using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1963717851)]
    public class TLWallPaperNoFile : TLAbsWallPaper
    {
        public override int Constructor
        {
            get
            {
                return -1963717851;
            }
        }

        public int Flags { get; set; }
        public bool Default { get; set; }
        public bool Dark { get; set; }
        public TLWallPaperSettings Settings { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Default = (Flags & 2) != 0;
            Dark = (Flags & 16) != 0;
            if ((Flags & 4) != 0)
                Settings = (TLWallPaperSettings)ObjectUtils.DeserializeObject(br);
            else
                Settings = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(Settings, bw);

        }
    }
}
