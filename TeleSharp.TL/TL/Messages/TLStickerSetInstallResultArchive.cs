using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(904138920)]
    public class TLStickerSetInstallResultArchive : TLAbsStickerSetInstallResult
    {
        public override int Constructor => 904138920;

        public TLVector<TLAbsStickerSetCovered> sets { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            sets = ObjectUtils.DeserializeVector<TLAbsStickerSetCovered>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(sets, bw);
        }
    }
}