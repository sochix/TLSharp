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

        public int flags { get; set; }
        public long id { get; set; }
        public long access_hash { get; set; }
        public string short_name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public TLAbsPhoto photo { get; set; }
        public TLAbsDocument document { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = document != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            id = br.ReadInt64();
            access_hash = br.ReadInt64();
            short_name = StringUtil.Deserialize(br);
            title = StringUtil.Deserialize(br);
            description = StringUtil.Deserialize(br);
            photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            if ((flags & 1) != 0)
                document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            else
                document = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(id);
            bw.Write(access_hash);
            StringUtil.Serialize(short_name, bw);
            StringUtil.Serialize(title, bw);
            StringUtil.Serialize(description, bw);
            ObjectUtils.SerializeObject(photo, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(document, bw);

        }
    }
}
