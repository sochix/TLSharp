using System.IO;

namespace TeleSharp.TL
{
    [TLObject(342061462)]
    public class TLInputFileLocation : TLAbsInputFileLocation
    {
        public override int Constructor => 342061462;

        public long volume_id { get; set; }
        public int local_id { get; set; }
        public long secret { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            volume_id = br.ReadInt64();
            local_id = br.ReadInt32();
            secret = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(volume_id);
            bw.Write(local_id);
            bw.Write(secret);
        }
    }
}