using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-852477119)]
    public class TLStickerSet : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -852477119;
            }
        }

        public int Flags { get; set; }
        public bool Installed { get; set; }
        public bool Archived { get; set; }
        public bool Official { get; set; }
        public bool Masks { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public int Count { get; set; }
        public int Hash { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Installed ? (Flags | 1) : (Flags & ~1);
            Flags = Archived ? (Flags | 2) : (Flags & ~2);
            Flags = Official ? (Flags | 4) : (Flags & ~4);
            Flags = Masks ? (Flags | 8) : (Flags & ~8);

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
            ComputeFlags();
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
