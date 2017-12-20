using System.IO;

namespace TeleSharp.TL
{
    [TLObject(2139689491)]
    public class TLUpdateWebPage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 2139689491;
            }
        }

        public int Pts { get; set; }

        public int PtsCount { get; set; }

        public TLAbsWebPage Webpage { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Webpage = (TLAbsWebPage)ObjectUtils.DeserializeObject(br);
            Pts = br.ReadInt32();
            PtsCount = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Webpage, bw);
            bw.Write(Pts);
            bw.Write(PtsCount);
        }
    }
}
