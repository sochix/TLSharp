using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1118798639)]
    public class TLInputThemeSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1118798639;
            }
        }

        public int Flags { get; set; }
        public TLAbsBaseTheme BaseTheme { get; set; }
        public int AccentColor { get; set; }
        public int? MessageTopColor { get; set; }
        public int? MessageBottomColor { get; set; }
        public TLAbsInputWallPaper Wallpaper { get; set; }
        public TLWallPaperSettings WallpaperSettings { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            BaseTheme = (TLAbsBaseTheme)ObjectUtils.DeserializeObject(br);
            AccentColor = br.ReadInt32();
            if ((Flags & 1) != 0)
                MessageTopColor = br.ReadInt32();
            else
                MessageTopColor = null;

            if ((Flags & 1) != 0)
                MessageBottomColor = br.ReadInt32();
            else
                MessageBottomColor = null;

            if ((Flags & 2) != 0)
                Wallpaper = (TLAbsInputWallPaper)ObjectUtils.DeserializeObject(br);
            else
                Wallpaper = null;

            if ((Flags & 2) != 0)
                WallpaperSettings = (TLWallPaperSettings)ObjectUtils.DeserializeObject(br);
            else
                WallpaperSettings = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(BaseTheme, bw);
            bw.Write(AccentColor);
            if ((Flags & 1) != 0)
                bw.Write(MessageTopColor.Value);
            if ((Flags & 1) != 0)
                bw.Write(MessageBottomColor.Value);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(Wallpaper, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(WallpaperSettings, bw);

        }
    }
}
