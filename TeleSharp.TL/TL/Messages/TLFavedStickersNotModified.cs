using System.IO;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1634752813)]
    public class TLFavedStickersNotModified : TLAbsFavedStickers
    {
        public override int Constructor
        {
            get
            {
                return -1634752813;
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
