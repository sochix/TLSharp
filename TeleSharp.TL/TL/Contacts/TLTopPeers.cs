using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(1891070632)]
    public class TLTopPeers : TLAbsTopPeers
    {
        public TLVector<TLTopPeerCategoryPeers> Categories { get; set; }

        public TLVector<TLAbsChat> Chats { get; set; }

        public override int Constructor
        {
            get
            {
                return 1891070632;
            }
        }

        public TLVector<TLAbsUser> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Categories = (TLVector<TLTopPeerCategoryPeers>)ObjectUtils.DeserializeVector<TLTopPeerCategoryPeers>(br);
            Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Categories, bw);
            ObjectUtils.SerializeObject(Chats, bw);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
