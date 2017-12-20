using System.IO;

namespace TeleSharp.TL
{
    [TLObject(628472761)]
    public class TLUpdateContactRegistered : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 628472761;
            }
        }

        public int Date { get; set; }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            Date = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            bw.Write(Date);
        }
    }
}
