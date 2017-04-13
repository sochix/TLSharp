using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-1878523231)]
    public class TLBlockedSlice : TLAbsBlocked
    {
        public override int Constructor => -1878523231;

        public int count { get; set; }
        public TLVector<TLContactBlocked> blocked { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            count = br.ReadInt32();
            blocked = ObjectUtils.DeserializeVector<TLContactBlocked>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(count);
            ObjectUtils.SerializeObject(blocked, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}