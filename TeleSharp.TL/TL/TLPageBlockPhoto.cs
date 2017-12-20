using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-372860542)]
    public class TLPageBlockPhoto : TLAbsPageBlock
    {
        public TLAbsRichText Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return -372860542;
            }
        }

        public long PhotoId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhotoId = br.ReadInt64();
            Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(PhotoId);
            ObjectUtils.SerializeObject(Caption, bw);
        }
    }
}
