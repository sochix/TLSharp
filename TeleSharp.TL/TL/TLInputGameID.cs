using System.IO;

namespace TeleSharp.TL
{
    [TLObject(53231223)]
    public class TLInputGameID : TLAbsInputGame
    {
        public long AccessHash { get; set; }

        public override int Constructor
        {
            get
            {
                return 53231223;
            }
        }

        public long Id { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt64();
            AccessHash = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(AccessHash);
        }
    }
}
