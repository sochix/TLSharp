using System.IO;

namespace TeleSharp.TL
{
    [TLObject(537022650)]
    public class TLUserEmpty : TLAbsUser
    {
        public override int Constructor
        {
            get
            {
                return 537022650;
            }
        }

        public int Id { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
        }
    }
}
