using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1218642516)]
    public class TLReplyInlineMarkup : TLAbsReplyMarkup
    {
        public override int Constructor
        {
            get
            {
                return 1218642516;
            }
        }

        public TLVector<TLKeyboardButtonRow> Rows { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Rows = (TLVector<TLKeyboardButtonRow>)ObjectUtils.DeserializeVector<TLKeyboardButtonRow>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Rows, bw);
        }
    }
}
