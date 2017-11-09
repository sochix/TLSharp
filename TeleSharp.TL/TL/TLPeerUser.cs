using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1649296275)]
    public class TLPeerUser : TLAbsPeer
    {
        public override int Constructor
        {
            get
            {
                return -1649296275;
            }
        }

        public int user_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);

        }
    }
}
