using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
	[TLObject(1891070632)]
    public class TLTopPeers : TLAbsTopPeers
    {

		
        public override int Constructor
        {
            get
            {
                return 1891070632;
            }
        }

             public TLVector<TLTopPeerCategoryPeers> categories {get;set;}
     public TLVector<TLAbsChat> chats {get;set;}
     public TLVector<TLAbsUser> users {get;set;}

		public TLTopPeers (){}
		public TLTopPeers (TLVector<TLTopPeerCategoryPeers> categories ,TLVector<TLAbsChat> chats ,TLVector<TLAbsUser> users ){
			this.categories = categories; 
this.chats = chats; 
this.users = users; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            categories = (TLVector<TLTopPeerCategoryPeers>)ObjectUtils.DeserializeVector<TLTopPeerCategoryPeers>(br);
chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(categories,bw);
ObjectUtils.SerializeObject(chats,bw);
ObjectUtils.SerializeObject(users,bw);

        }
    }
}
