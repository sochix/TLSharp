using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1218642516)]
    public class TLReplyInlineMarkup : TLAbsReplyMarkup
    {
        public override int Constructor => 1218642516;

        public TLVector<TLKeyboardButtonRow> rows { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            rows = ObjectUtils.DeserializeVector<TLKeyboardButtonRow>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(rows, bw);
        }
    }
}