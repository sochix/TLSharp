using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1836524247)]
    public class TLPhoto : TLAbsPhoto
    {
        public long AccessHash { get; set; }

        public override int Constructor
        {
            get
            {
                return -1836524247;
            }
        }

        public int Date { get; set; }

        public int Flags { get; set; }

        public bool HasStickers { get; set; }

        public long Id { get; set; }

        public TLVector<TLAbsPhotoSize> Sizes { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            HasStickers = (Flags & 1) != 0;
            Id = br.ReadInt64();
            AccessHash = br.ReadInt64();
            Date = br.ReadInt32();
            Sizes = (TLVector<TLAbsPhotoSize>)ObjectUtils.DeserializeVector<TLAbsPhotoSize>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(Id);
            bw.Write(AccessHash);
            bw.Write(Date);
            ObjectUtils.SerializeObject(Sizes, bw);
        }
    }
}
