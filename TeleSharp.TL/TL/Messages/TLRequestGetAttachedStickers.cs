using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-866424884)]
    public class TLRequestGetAttachedStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -866424884;
            }
        }

        public TLAbsInputStickeredMedia media { get; set; }
        public TLVector<TLAbsStickerSetCovered> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            media = (TLAbsInputStickeredMedia)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(media, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLAbsStickerSetCovered>)ObjectUtils.DeserializeVector<TLAbsStickerSetCovered>(br);

        }
    }
}
