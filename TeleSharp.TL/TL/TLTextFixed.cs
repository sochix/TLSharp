using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1816074681)]
    public class TLTextFixed : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return 1816074681;
            }
        }

        public TLAbsRichText text { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(text, bw);

        }
    }
}
