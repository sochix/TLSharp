using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(2002815875)]
    public class TLKeyboardButtonRow : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 2002815875;
            }
        }

        public TLVector<TLAbsKeyboardButton> Buttons { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Buttons = (TLVector<TLAbsKeyboardButton>)ObjectUtils.DeserializeVector<TLAbsKeyboardButton>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Buttons, bw);
        }
    }
}
