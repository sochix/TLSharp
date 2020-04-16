using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Account
{
    [TLObject(-57811990)]
    public class TLRequestGetWallPaper : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -57811990;
            }
        }

        public TLAbsInputWallPaper Wallpaper { get; set; }
        public TLAbsWallPaper Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Wallpaper = (TLAbsInputWallPaper)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Wallpaper, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsWallPaper)ObjectUtils.DeserializeObject(br);
        }
    }
}
