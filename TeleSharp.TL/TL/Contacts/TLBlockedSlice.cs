using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-1878523231)]
    public class TLBlockedSlice : TLAbsBlocked
    {
        public TLVector<TLContactBlocked> Blocked { get; set; }

        public override int Constructor
        {
            get
            {
                return -1878523231;
            }
        }

        public int Count { get; set; }

        public TLVector<TLAbsUser> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Count = br.ReadInt32();
            Blocked = (TLVector<TLContactBlocked>)ObjectUtils.DeserializeVector<TLContactBlocked>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Count);
            ObjectUtils.SerializeObject(Blocked, bw);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
