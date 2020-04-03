using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1987480557)]
    public class TLPageBlockDetails : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 1987480557;
            }
        }

        public int Flags { get; set; }
        public bool Open { get; set; }
        public TLVector<TLAbsPageBlock> Blocks { get; set; }
        public TLAbsRichText Title { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Open = (Flags & 1) != 0;
            Blocks = (TLVector<TLAbsPageBlock>)ObjectUtils.DeserializeVector<TLAbsPageBlock>(br);
            Title = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Blocks, bw);
            ObjectUtils.SerializeObject(Title, bw);

        }
    }
}
