using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-805141448)]
    public class TLImportedContact : TLObject
    {
        public long ClientId { get; set; }

        public override int Constructor
        {
            get
            {
                return -805141448;
            }
        }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            ClientId = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            bw.Write(ClientId);
        }
    }
}
