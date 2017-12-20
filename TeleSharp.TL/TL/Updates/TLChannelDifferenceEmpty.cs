using System.IO;

namespace TeleSharp.TL.Updates
{
    [TLObject(1041346555)]
    public class TLChannelDifferenceEmpty : TLAbsChannelDifference
    {
        public override int Constructor
        {
            get
            {
                return 1041346555;
            }
        }

        public bool Final { get; set; }

        public int Flags { get; set; }

        public int Pts { get; set; }

        public int? Timeout { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Final = (Flags & 1) != 0;
            Pts = br.ReadInt32();
            if ((Flags & 2) != 0)
                Timeout = br.ReadInt32();
            else
                Timeout = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(Pts);
            if ((Flags & 2) != 0)
                bw.Write(Timeout.Value);
        }
    }
}
