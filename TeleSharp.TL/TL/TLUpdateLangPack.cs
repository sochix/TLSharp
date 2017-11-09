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

        public TLLangPackDifference difference { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            difference = (TLLangPackDifference)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(difference, bw);

        }
    }
}
