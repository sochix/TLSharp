using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-852477119)]
    public class TLStickerSet : TLObject
    {
        public long AccessHash { get; set; }

        public bool Archived { get; set; }

        public override int Constructor
        {
            get
            {
                return -852477119;
            }
        }

        public int Count { get; set; }

        public int Flags { get; set; }

        public int Hash { get; set; }

        public long Id { get; set; }

        public bool Installed { get; set; }

        public bool Masks { get; set; }

        public bool Official { get; set; }

        public string ShortName { get; set; }

        public string Title { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Installed = (Flags & 1) != 0;
            Archived = (Flags & 2) != 0;
            Official = (Flags & 4) != 0;
            Masks = (Flags & 8) != 0;
            Id = br.ReadInt64();
            AccessHash = br.ReadInt64();
            Title = StringUtil.Deserialize(br);
            ShortName = StringUtil.Deserialize(br);
            Count = br.ReadInt32();
            Hash = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);




            bw.Write(Id);
            bw.Write(AccessHash);
            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(ShortName, bw);
            bw.Write(Count);
            bw.Write(Hash);
        }
    }
}
