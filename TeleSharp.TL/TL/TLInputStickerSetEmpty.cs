using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-4838507)]
    public class TLInputStickerSetEmpty : TLAbsInputStickerSet
    {
        public override int Constructor
        {
            get
            {
                return -4838507;
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
