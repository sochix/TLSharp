using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1251338318)]
    public class TLInputNotifyChats : TLAbsInputNotifyPeer
    {
        public override int Constructor
        {
            get
            {
                return 1251338318;
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
