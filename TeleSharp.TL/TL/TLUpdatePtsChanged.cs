using System.IO;
namespace TeleSharp.TL
{
    [TLObject(861169551)]
    public class TLUpdatePtsChanged : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 861169551;
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
