using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-200242528)]
    public class TLReplyKeyboardForceReply : TLAbsReplyMarkup
    {
        public override int Constructor
        {
            get
            {
                return -200242528;
            }
        }

        public int Flags { get; set; }

        public bool Selective { get; set; }

        public bool SingleUse { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            SingleUse = (Flags & 2) != 0;
            Selective = (Flags & 4) != 0;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
        }
    }
}
