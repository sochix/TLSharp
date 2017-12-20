using System.IO;

namespace TeleSharp.TL
{
    [TLObject(342061462)]
    public class TLInputFileLocation : TLAbsInputFileLocation
    {
        public override int Constructor
        {
            get
            {
                return 342061462;
            }
        }

        public int LocalId { get; set; }

        public long Secret { get; set; }

        public long VolumeId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            VolumeId = br.ReadInt64();
            LocalId = br.ReadInt32();
            Secret = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(VolumeId);
            bw.Write(LocalId);
            bw.Write(Secret);
        }
    }
}
