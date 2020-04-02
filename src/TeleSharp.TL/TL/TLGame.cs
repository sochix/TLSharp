using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1107729093)]
    public class TLGame : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1107729093;
            }
        }

        public int Flags { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }
        public string ShortName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLAbsPhoto Photo { get; set; }
        public TLAbsDocument Document { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Document != null ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Id = br.ReadInt64();
            AccessHash = br.ReadInt64();
            ShortName = StringUtil.Deserialize(br);
            Title = StringUtil.Deserialize(br);
            Description = StringUtil.Deserialize(br);
            Photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            else
                Document = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            bw.Write(Id);
            bw.Write(AccessHash);
            StringUtil.Serialize(ShortName, bw);
            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(Description, bw);
            ObjectUtils.SerializeObject(Photo, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Document, bw);

        }
    }
}
