using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1433323434)]
    public class TLPageFull : TLAbsPage
    {
        public override int Constructor
        {
            get
            {
                return 1433323434;
            }
        }

        public TLVector<TLAbsPageBlock> blocks { get; set; }
        public TLVector<TLAbsPhoto> photos { get; set; }
        public TLVector<TLAbsDocument> documents { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            blocks = (TLVector<TLAbsPageBlock>)ObjectUtils.DeserializeVector<TLAbsPageBlock>(br);
            photos = (TLVector<TLAbsPhoto>)ObjectUtils.DeserializeVector<TLAbsPhoto>(br);
            documents = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(blocks, bw);
            ObjectUtils.SerializeObject(photos, bw);
            ObjectUtils.SerializeObject(documents, bw);

        }
    }
}
