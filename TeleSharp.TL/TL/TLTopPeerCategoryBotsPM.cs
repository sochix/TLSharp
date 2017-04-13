using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1419371685)]
    public class TLTopPeerCategoryBotsPM : TLAbsTopPeerCategory
    {
        public override int Constructor => -1419371685;


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