using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-524237339)]
    public class TLPageTableRow : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -524237339;
            }
        }

        public TLVector<TLPageTableCell> Cells { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Cells = (TLVector<TLPageTableCell>)ObjectUtils.DeserializeVector<TLPageTableCell>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Cells, bw);
        }
    }
}
