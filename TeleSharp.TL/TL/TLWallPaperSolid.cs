using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1662091044)]
    public class TLWallPaperSolid : TLAbsWallPaper
    {
        public override int Constructor => 1662091044;

        public int id { get; set; }
        public string title { get; set; }
        public int bg_color { get; set; }
        public int color { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
            title = StringUtil.Deserialize(br);
            bg_color = br.ReadInt32();
            color = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            StringUtil.Serialize(title, bw);
            bw.Write(bg_color);
            bw.Write(color);
        }
    }
}