using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(649453030)]
    public class TLMessageEditData : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 649453030;
            }
        }

        public int flags { get; set; }
        public bool caption { get; set; }

        public void ComputeFlags()
        {
            flags = 0;
            flags = caption ? (flags | 1) : (flags & ~1);
        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            caption = (flags & 1) != 0;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
        }
    }
}