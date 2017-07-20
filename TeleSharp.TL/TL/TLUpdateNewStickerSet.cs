using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1753886890)]
    public class TLUpdateNewStickerSet : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1753886890;
            }
        }

        public Messages.TLStickerSet stickerset { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            stickerset = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(stickerset, bw);

        }
    }
}
