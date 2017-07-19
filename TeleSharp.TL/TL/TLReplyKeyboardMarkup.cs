using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(889353612)]
    public class TLReplyKeyboardMarkup : TLAbsReplyMarkup
    {
        public override int Constructor
        {
            get
            {
                return 889353612;
            }
        }

        public int flags { get; set; }
        public bool resize { get; set; }
        public bool single_use { get; set; }
        public bool selective { get; set; }
        public TLVector<TLKeyboardButtonRow> rows { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = resize ? (flags | 1) : (flags & ~1);
            flags = single_use ? (flags | 2) : (flags & ~2);
            flags = selective ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            resize = (flags & 1) != 0;
            single_use = (flags & 2) != 0;
            selective = (flags & 4) != 0;
            rows = (TLVector<TLKeyboardButtonRow>)ObjectUtils.DeserializeVector<TLKeyboardButtonRow>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);



            ObjectUtils.SerializeObject(rows, bw);

        }
    }
}
