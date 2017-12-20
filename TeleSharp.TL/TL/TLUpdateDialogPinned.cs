using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-686710068)]
    public class TLUpdateDialogPinned : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -686710068;
            }
        }

        public int Flags { get; set; }

        public TLAbsPeer Peer { get; set; }

        public bool Pinned { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Pinned = (Flags & 1) != 0;
            Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Peer, bw);
        }
    }
}
