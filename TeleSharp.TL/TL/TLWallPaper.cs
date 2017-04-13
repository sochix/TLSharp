using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-860866985)]
    public class TLWallPaper : TLAbsWallPaper
    {
        public override int Constructor => -860866985;

        public int id { get; set; }
        public string title { get; set; }
        public TLVector<TLAbsPhotoSize> sizes { get; set; }
        public int color { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
            title = StringUtil.Deserialize(br);
            sizes = ObjectUtils.DeserializeVector<TLAbsPhotoSize>(br);
            color = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            StringUtil.Serialize(title, bw);
            ObjectUtils.SerializeObject(sizes, bw);
            bw.Write(color);
        }
    }
}