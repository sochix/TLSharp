using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1889961234)]
    public class TLPeerNotifySettingsEmpty : TLAbsPeerNotifySettings
    {
        public override int Constructor
        {
            get
            {
                return 1889961234;
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
