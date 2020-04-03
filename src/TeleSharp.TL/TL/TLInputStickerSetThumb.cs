using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(230353641)]
    public class TLInputStickerSetThumb : TLAbsInputFileLocation
    {
        public override int Constructor
        {
            get
            {
                return 230353641;
            }
        }

        public TLAbsInputStickerSet Stickerset { get; set; }
        public long VolumeId { get; set; }
        public int LocalId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            VolumeId = br.ReadInt64();
            LocalId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Stickerset, bw);
            bw.Write(VolumeId);
            bw.Write(LocalId);

        }
    }
}
