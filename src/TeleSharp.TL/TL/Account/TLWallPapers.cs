using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1881892265)]
    public class TLWallPapers : TLAbsWallPapers
    {
        public override int Constructor
        {
            get
            {
                return 1881892265;
            }
        }

        public int Hash { get; set; }
        public TLVector<TLAbsWallPaper> Wallpapers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt32();
            Wallpapers = (TLVector<TLAbsWallPaper>)ObjectUtils.DeserializeVector<TLAbsWallPaper>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);
            ObjectUtils.SerializeObject(Wallpapers, bw);

        }
    }
}
