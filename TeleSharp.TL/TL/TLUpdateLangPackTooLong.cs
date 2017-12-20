using System.IO;

namespace TeleSharp.TL
{
    [TLObject(281165899)]
    public class TLUpdateLangPackTooLong : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 281165899;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }
    }
}
