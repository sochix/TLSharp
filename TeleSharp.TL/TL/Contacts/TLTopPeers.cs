using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(1891070632)]
    public class TLTopPeers : TLAbsTopPeers
    {
        public override int Constructor => 1891070632;

        public TLVector<TLTopPeerCategoryPeers> categories { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            categories = ObjectUtils.DeserializeVector<TLTopPeerCategoryPeers>(br);
            chats = ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(categories, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}