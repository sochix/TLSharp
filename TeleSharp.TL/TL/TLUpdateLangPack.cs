using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1442983757)]
    public class TLUpdateLangPack : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1442983757;
            }
        }

        public TLLangPackDifference Difference { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Difference = (TLLangPackDifference)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Difference, bw);
        }
    }
}
