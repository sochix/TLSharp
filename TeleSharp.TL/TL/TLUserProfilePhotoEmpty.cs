using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1326562017)]
    public class TLUserProfilePhotoEmpty : TLAbsUserProfilePhoto
    {
        public override int Constructor => 1326562017;


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