using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-567906571)]
    public class TLTopPeersNotModified : TLAbsTopPeers
    {
        public override int Constructor
        {
            get
            {
                return -567906571;
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
