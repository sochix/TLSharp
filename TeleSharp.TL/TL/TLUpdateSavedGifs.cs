using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1821035490)]
    public class TLUpdateSavedGifs : TLAbsUpdate
    {
        public override int Constructor => -1821035490;


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