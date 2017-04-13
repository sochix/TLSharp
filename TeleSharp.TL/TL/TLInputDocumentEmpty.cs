using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1928391342)]
    public class TLInputDocumentEmpty : TLAbsInputDocument
    {
        public override int Constructor => 1928391342;


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