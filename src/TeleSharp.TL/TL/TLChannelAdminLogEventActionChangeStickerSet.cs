using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1312568665)]
    public class TLChannelAdminLogEventActionChangeStickerSet : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return -1312568665;
            }
        }

        public TLAbsInputStickerSet PrevStickerset { get; set; }
        public TLAbsInputStickerSet NewStickerset { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            PrevStickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            NewStickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PrevStickerset, bw);
            ObjectUtils.SerializeObject(NewStickerset, bw);

        }
    }
}
