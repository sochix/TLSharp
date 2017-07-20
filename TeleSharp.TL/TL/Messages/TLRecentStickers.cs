using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1558317424)]
    public class TLRecentStickers : TLAbsRecentStickers
    {
        public override int Constructor
        {
            get
            {
                return 1558317424;
            }
        }

        public int hash { get; set; }
        public TLVector<TLAbsDocument> stickers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt32();
            stickers = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(hash);
            ObjectUtils.SerializeObject(stickers, bw);

        }
    }
}
