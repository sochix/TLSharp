using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-971322408)]
    public class TLWebDocument : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -971322408;
            }
        }

        public string url { get; set; }
        public long access_hash { get; set; }
        public int size { get; set; }
        public string mime_type { get; set; }
        public TLVector<TLAbsDocumentAttribute> attributes { get; set; }
        public int dc_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            access_hash = br.ReadInt64();
            size = br.ReadInt32();
            mime_type = StringUtil.Deserialize(br);
            attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);
            dc_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            bw.Write(access_hash);
            bw.Write(size);
            StringUtil.Serialize(mime_type, bw);
            ObjectUtils.SerializeObject(attributes, bw);
            bw.Write(dc_id);

        }
    }
}
