using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1918567619)]
    public class TLUpdatesCombined : TLAbsUpdates
    {
        public override int Constructor => 1918567619;

        public TLVector<TLAbsUpdate> updates { get; set; }
        public TLVector<TLAbsUser> users { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public int date { get; set; }
        public int seq_start { get; set; }
        public int seq { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            updates = ObjectUtils.DeserializeVector<TLAbsUpdate>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
            chats = ObjectUtils.DeserializeVector<TLAbsChat>(br);
            date = br.ReadInt32();
            seq_start = br.ReadInt32();
            seq = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(updates, bw);
            ObjectUtils.SerializeObject(users, bw);
            ObjectUtils.SerializeObject(chats, bw);
            bw.Write(date);
            bw.Write(seq_start);
            bw.Write(seq);
        }
    }
}