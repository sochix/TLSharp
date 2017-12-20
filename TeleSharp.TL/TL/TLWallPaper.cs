using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-860866985)]
    public class TLWallPaper : TLAbsWallPaper
    {
        public int Color { get; set; }

        public override int Constructor
        {
            get
            {
                return -860866985;
            }
        }

        public int Id { get; set; }

        public TLVector<TLAbsPhotoSize> Sizes { get; set; }

        public string Title { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
            Title = StringUtil.Deserialize(br);
            Sizes = (TLVector<TLAbsPhotoSize>)ObjectUtils.DeserializeVector<TLAbsPhotoSize>(br);
            Color = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            StringUtil.Serialize(Title, bw);
            ObjectUtils.SerializeObject(Sizes, bw);
            bw.Write(Color);
        }
    }
}
