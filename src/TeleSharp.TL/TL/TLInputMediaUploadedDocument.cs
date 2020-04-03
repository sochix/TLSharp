using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1530447553)]
    public class TLInputMediaUploadedDocument : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 1530447553;
            }
        }

        public int Flags { get; set; }
        public bool NosoundVideo { get; set; }
        public TLAbsInputFile File { get; set; }
        public TLAbsInputFile Thumb { get; set; }
        public string MimeType { get; set; }
        public TLVector<TLAbsDocumentAttribute> Attributes { get; set; }
        public TLVector<TLAbsInputDocument> Stickers { get; set; }
        public int? TtlSeconds { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            NosoundVideo = (Flags & 8) != 0;
            File = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            if ((Flags & 4) != 0)
                Thumb = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            else
                Thumb = null;

            MimeType = StringUtil.Deserialize(br);
            Attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);
            if ((Flags & 1) != 0)
                Stickers = (TLVector<TLAbsInputDocument>)ObjectUtils.DeserializeVector<TLAbsInputDocument>(br);
            else
                Stickers = null;

            if ((Flags & 2) != 0)
                TtlSeconds = br.ReadInt32();
            else
                TtlSeconds = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(File, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(Thumb, bw);
            StringUtil.Serialize(MimeType, bw);
            ObjectUtils.SerializeObject(Attributes, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Stickers, bw);
            if ((Flags & 2) != 0)
                bw.Write(TtlSeconds.Value);

        }
    }
}
