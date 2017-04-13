using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1744710921)]
    public class TLDocumentAttributeHasStickers : TLAbsDocumentAttribute
    {
        public override int Constructor => -1744710921;


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