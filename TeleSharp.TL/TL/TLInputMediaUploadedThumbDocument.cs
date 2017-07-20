using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1356369070)]
    public class TLInputMediaUploadedThumbDocument : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 1356369070;
            }
        }

        public int flags { get; set; }
        public TLAbsInputFile file { get; set; }
        public TLAbsInputFile thumb { get; set; }
        public string mime_type { get; set; }
        public TLVector<TLAbsDocumentAttribute> attributes { get; set; }
        public string caption { get; set; }
        public TLVector<TLAbsInputDocument> stickers { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = stickers != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            file = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            thumb = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            mime_type = StringUtil.Deserialize(br);
            attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);
            caption = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                stickers = (TLVector<TLAbsInputDocument>)ObjectUtils.DeserializeVector<TLAbsInputDocument>(br);
            else
                stickers = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            ObjectUtils.SerializeObject(file, bw);
            ObjectUtils.SerializeObject(thumb, bw);
            StringUtil.Serialize(mime_type, bw);
            ObjectUtils.SerializeObject(attributes, bw);
            StringUtil.Serialize(caption, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(stickers, bw);

        }
    }
}
