using System.IO;

namespace TeleSharp.TL
{
    [TLObject(681706865)]
    public class TLMessageEntityCode : TLAbsMessageEntity
    {
        public override int Constructor => 681706865;

        public int offset { get; set; }
        public int length { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            offset = br.ReadInt32();
            length = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(offset);
            bw.Write(length);
        }
    }
}