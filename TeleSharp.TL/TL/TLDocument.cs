using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2027738169)]
    public class TLDocument : TLAbsDocument
    {
        public override int Constructor
        {
            get
            {
                return -2027738169;
            }
        }

        public long id { get; set; }
        public long access_hash { get; set; }
        public int date { get; set; }
        public string mime_type { get; set; }
        public int size { get; set; }
        public TLAbsPhotoSize thumb { get; set; }
        public int dc_id { get; set; }
        public int version { get; set; }
        public TLVector<TLAbsDocumentAttribute> attributes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            access_hash = br.ReadInt64();
            date = br.ReadInt32();
            mime_type = StringUtil.Deserialize(br);
            size = br.ReadInt32();
            thumb = (TLAbsPhotoSize)ObjectUtils.DeserializeObject(br);
            dc_id = br.ReadInt32();
            version = br.ReadInt32();
            attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(access_hash);
            bw.Write(date);
            StringUtil.Serialize(mime_type, bw);
            bw.Write(size);
            ObjectUtils.SerializeObject(thumb, bw);
            bw.Write(dc_id);
            bw.Write(version);
            ObjectUtils.SerializeObject(attributes, bw);

        }
    }
}
