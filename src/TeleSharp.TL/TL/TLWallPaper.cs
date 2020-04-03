using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1539849235)]
    public class TLWallPaper : TLAbsWallPaper
    {
        public override int Constructor
        {
            get
            {
                return -1539849235;
            }
        }

        public long Id { get; set; }
        public int Flags { get; set; }
        public bool Creator { get; set; }
        public bool Default { get; set; }
        public bool Pattern { get; set; }
        public bool Dark { get; set; }
        public long AccessHash { get; set; }
        public string Slug { get; set; }
        public TLAbsDocument Document { get; set; }
        public TLWallPaperSettings Settings { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt64();
            Flags = br.ReadInt32();
            Creator = (Flags & 1) != 0;
            Default = (Flags & 2) != 0;
            Pattern = (Flags & 8) != 0;
            Dark = (Flags & 16) != 0;
            AccessHash = br.ReadInt64();
            Slug = StringUtil.Deserialize(br);
            Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            if ((Flags & 4) != 0)
                Settings = (TLWallPaperSettings)ObjectUtils.DeserializeObject(br);
            else
                Settings = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(Flags);




            bw.Write(AccessHash);
            StringUtil.Serialize(Slug, bw);
            ObjectUtils.SerializeObject(Document, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(Settings, bw);

        }
    }
}
