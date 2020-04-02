using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(301019932)]
    public class TLUpdateShortSentMessage : TLAbsUpdates
    {
        public override int Constructor
        {
            get
            {
                return 301019932;
            }
        }

        public int Flags { get; set; }
        public bool Out { get; set; }
        public int Id { get; set; }
        public int Pts { get; set; }
        public int PtsCount { get; set; }
        public int Date { get; set; }
        public TLAbsMessageMedia Media { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Out ? (Flags | 2) : (Flags & ~2);
            Flags = Media != null ? (Flags | 512) : (Flags & ~512);
            Flags = Entities != null ? (Flags | 128) : (Flags & ~128);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Out = (Flags & 2) != 0;
            Id = br.ReadInt32();
            Pts = br.ReadInt32();
            PtsCount = br.ReadInt32();
            Date = br.ReadInt32();
            if ((Flags & 512) != 0)
                Media = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
            else
                Media = null;

            if ((Flags & 128) != 0)
                Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                Entities = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            bw.Write(Id);
            bw.Write(Pts);
            bw.Write(PtsCount);
            bw.Write(Date);
            if ((Flags & 512) != 0)
                ObjectUtils.SerializeObject(Media, bw);
            if ((Flags & 128) != 0)
                ObjectUtils.SerializeObject(Entities, bw);

        }
    }
}
