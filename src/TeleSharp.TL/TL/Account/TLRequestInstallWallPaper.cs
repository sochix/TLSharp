using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-18000023)]
    public class TLRequestInstallWallPaper : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -18000023;
            }
        }

        public TLAbsInputWallPaper Wallpaper { get; set; }
        public TLWallPaperSettings Settings { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Wallpaper = (TLAbsInputWallPaper)ObjectUtils.DeserializeObject(br);
            Settings = (TLWallPaperSettings)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Wallpaper, bw);
            ObjectUtils.SerializeObject(Settings, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
