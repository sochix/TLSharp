using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1085412734)]
    public class TLPageBlockTable : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1085412734;
            }
        }

        public int Flags { get; set; }
        public bool Bordered { get; set; }
        public bool Striped { get; set; }
        public TLAbsRichText Title { get; set; }
        public TLVector<TLPageTableRow> Rows { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Bordered = (Flags & 1) != 0;
            Striped = (Flags & 2) != 0;
            Title = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            Rows = (TLVector<TLPageTableRow>)ObjectUtils.DeserializeVector<TLPageTableRow>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            ObjectUtils.SerializeObject(Title, bw);
            ObjectUtils.SerializeObject(Rows, bw);

        }
    }
}
