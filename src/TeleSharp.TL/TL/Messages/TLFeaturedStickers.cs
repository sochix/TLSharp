using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-123893531)]
    public class TLFeaturedStickers : TLAbsFeaturedStickers
    {
        public override int Constructor
        {
            get
            {
                return -123893531;
            }
        }

        public int Hash { get; set; }
        public TLVector<TLAbsStickerSetCovered> Sets { get; set; }
        public TLVector<long> Unread { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt32();
            Sets = (TLVector<TLAbsStickerSetCovered>)ObjectUtils.DeserializeVector<TLAbsStickerSetCovered>(br);
            Unread = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);
            ObjectUtils.SerializeObject(Sets, bw);
            ObjectUtils.SerializeObject(Unread, bw);

        }
    }
}
