using System.IO;

namespace TeleSharp.TL
{
    [TLObject(53231223)]
    public class TLInputGameID : TLAbsInputGame
    {
        public override int Constructor
        {
            get
            {
                return 53231223;
            }
        }

        public long id { get; set; }
        public long access_hash { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            access_hash = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(access_hash);
        }
    }
}