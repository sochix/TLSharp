using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(649453030)]
    public class TLMessageEditData : TLObject
    {
        public bool Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return 649453030;
            }
        }

        public int Flags { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Caption = (Flags & 1) != 0;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
        }
    }
}
