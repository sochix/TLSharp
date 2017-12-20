using System.IO;

namespace TeleSharp.TL
{
    [TLObject(2120376535)]
    public class TLTextConcat : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return 2120376535;
            }
        }

        public TLVector<TLAbsRichText> Texts { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Texts = (TLVector<TLAbsRichText>)ObjectUtils.DeserializeVector<TLAbsRichText>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Texts, bw);
        }
    }
}
