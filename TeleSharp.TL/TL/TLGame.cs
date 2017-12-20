using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1107729093)]
    public class TLGame : TLObject
    {
        public long AccessHash { get; set; }

        public override int Constructor
        {
            get
            {
                return -1107729093;
            }
        }

        public string Description { get; set; }

        public TLAbsDocument Document { get; set; }

        public int Flags { get; set; }

        public long Id { get; set; }

        public TLAbsPhoto Photo { get; set; }

        public string ShortName { get; set; }

        public string Title { get; set; }

        public void ComputeFlags()
        {
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
