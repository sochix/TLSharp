using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2131957734)]
    public class TLUpdateUserBlocked : TLAbsUpdate
    {
        public override int Constructor => -2131957734;

        public int user_id { get; set; }
        public bool blocked { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            blocked = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            BoolUtil.Serialize(blocked, bw);
        }
    }
}