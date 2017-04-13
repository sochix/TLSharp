using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(-1350696044)]
    public class TLAppChangelogEmpty : TLAbsAppChangelog
    {
        public override int Constructor => -1350696044;


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