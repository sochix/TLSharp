using System.IO;

namespace TeleSharp.TL
{
    [TLObject(182649427)]
    public class TLMessageRange : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 182649427;
            }
        }

        public int MaxId { get; set; }

        public int MinId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            MinId = br.ReadInt32();
            MaxId = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(MinId);
            bw.Write(MaxId);
        }
    }
}
