using System.IO;

namespace TeleSharp.TL
{
    [TLObject(646922073)]
    public class TLContactLinkHasPhone : TLAbsContactLink
    {
        public override int Constructor => 646922073;


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