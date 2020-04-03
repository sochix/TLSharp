using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(136105807)]
    public class TLTextImage : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return 136105807;
            }
        }

        public long DocumentId { get; set; }
        public int W { get; set; }
        public int H { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            DocumentId = br.ReadInt64();
            W = br.ReadInt32();
            H = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(DocumentId);
            bw.Write(W);
            bw.Write(H);

        }
    }
}
