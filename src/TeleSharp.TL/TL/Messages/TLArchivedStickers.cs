using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1338747336)]
    public class TLArchivedStickers : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1338747336;
            }
        }

        public int Count { get; set; }
        public TLVector<TLAbsStickerSetCovered> Sets { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Count = br.ReadInt32();
            Sets = (TLVector<TLAbsStickerSetCovered>)ObjectUtils.DeserializeVector<TLAbsStickerSetCovered>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Count);
            ObjectUtils.SerializeObject(Sets, bw);

        }
    }
}
