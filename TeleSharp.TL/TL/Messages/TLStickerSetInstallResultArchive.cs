using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(904138920)]
    public class TLStickerSetInstallResultArchive : TLAbsStickerSetInstallResult
    {
        public override int Constructor
        {
            get
            {
                return 904138920;
            }
        }

        public TLVector<TLAbsStickerSetCovered> Sets { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Sets = (TLVector<TLAbsStickerSetCovered>)ObjectUtils.DeserializeVector<TLAbsStickerSetCovered>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Sets, bw);
        }
    }
}
