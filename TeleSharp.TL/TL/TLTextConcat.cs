using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
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

        public TLVector<TLAbsRichText> texts { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            texts = (TLVector<TLAbsRichText>)ObjectUtils.DeserializeVector<TLAbsRichText>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(texts, bw);

        }
    }
}
