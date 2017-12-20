using System.IO;

namespace TeleSharp.TL
{
    [TLObject(9203775)]
    public class TLUserStatusOffline : TLAbsUserStatus
    {
        public override int Constructor
        {
            get
            {
                return 9203775;
            }
        }

        public int WasOnline { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            WasOnline = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(WasOnline);
        }
    }
}
