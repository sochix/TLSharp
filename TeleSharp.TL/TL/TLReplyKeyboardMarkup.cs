using System.IO;

namespace TeleSharp.TL
{
    [TLObject(889353612)]
    public class TLReplyKeyboardMarkup : TLAbsReplyMarkup
    {
        public override int Constructor
        {
            get
            {
                return 889353612;
            }
        }

        public int Flags { get; set; }

        public bool Resize { get; set; }

        public TLVector<TLKeyboardButtonRow> Rows { get; set; }

        public bool Selective { get; set; }

        public bool SingleUse { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Resize = (Flags & 1) != 0;
            SingleUse = (Flags & 2) != 0;
            Selective = (Flags & 4) != 0;
            Rows = (TLVector<TLKeyboardButtonRow>)ObjectUtils.DeserializeVector<TLKeyboardButtonRow>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



            ObjectUtils.SerializeObject(Rows, bw);
        }
    }
}
