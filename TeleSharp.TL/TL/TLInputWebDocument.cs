using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1678949555)]
    public class TLInputWebDocument : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1678949555;
            }
        }

        public string url { get; set; }
        public int size { get; set; }
        public string mime_type { get; set; }
        public TLVector<TLAbsDocumentAttribute> attributes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            size = br.ReadInt32();
            mime_type = StringUtil.Deserialize(br);
            attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            bw.Write(size);
            StringUtil.Serialize(mime_type, bw);
            ObjectUtils.SerializeObject(attributes, bw);

        }
    }
}
