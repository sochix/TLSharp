using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1815593308)]
    public class TLDocumentAttributeImageSize : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return 1815593308;
            }
        }

        public int W { get; set; }
        public int H { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            W = br.ReadInt32();
            H = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(W);
            bw.Write(H);

        }
    }
}
