using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-38694904)]
    public class TLMessageMediaGame : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return -38694904;
            }
        }

        public TLGame Game { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Game = (TLGame)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Game, bw);
        }
    }
}
