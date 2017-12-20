using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2131957734)]
    public class TLUpdateUserBlocked : TLAbsUpdate
    {
        public bool Blocked { get; set; }

        public override int Constructor
        {
            get
            {
                return -2131957734;
            }
        }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            Blocked = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            BoolUtil.Serialize(Blocked, bw);
        }
    }
}
