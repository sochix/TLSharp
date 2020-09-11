using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Account
{
    [TLObject(1705865692)]
    public class TLRequestGetMultiWallPapers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1705865692;
            }
        }

        public TLVector<TLAbsInputWallPaper> Wallpapers { get; set; }
        public TLVector<TLAbsWallPaper> Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Wallpapers = (TLVector<TLAbsInputWallPaper>)ObjectUtils.DeserializeVector<TLAbsInputWallPaper>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Wallpapers, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLAbsWallPaper>)ObjectUtils.DeserializeVector<TLAbsWallPaper>(br);
        }
    }
}
