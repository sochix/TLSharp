using System.IO;

namespace TeleSharp.TL.Langpack
{
    [TLObject(-2146445955)]
    public class TLRequestGetLanguages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2146445955;
            }
        }

        public TLVector<TLLangPackLanguage> Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLLangPackLanguage>)ObjectUtils.DeserializeVector<TLLangPackLanguage>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }
    }
}
