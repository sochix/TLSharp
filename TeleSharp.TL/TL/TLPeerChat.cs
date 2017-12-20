using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1160714821)]
    public class TLPeerChat : TLAbsPeer
    {
        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return -1160714821;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
        }
    }
}
