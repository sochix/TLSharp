using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1670052855)]
    public class TLFoundGifCached : TLAbsFoundGif
    {
        public override int Constructor => -1670052855;

        public string url { get; set; }
        public TLAbsPhoto photo { get; set; }
        public TLAbsDocument document { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            photo = (TLAbsPhoto) ObjectUtils.DeserializeObject(br);
            document = (TLAbsDocument) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            ObjectUtils.SerializeObject(photo, bw);
            ObjectUtils.SerializeObject(document, bw);
        }
    }
}