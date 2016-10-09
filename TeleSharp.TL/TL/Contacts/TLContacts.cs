using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
	[TLObject(1871416498)]
    public class TLContacts : TLAbsContacts
    {

		
        public override int Constructor
        {
            get
            {
                return 1871416498;
            }
        }

             public TLVector<TLContact> contacts {get;set;}
     public TLVector<TLAbsUser> users {get;set;}

		public TLContacts (){}
		public TLContacts (TLVector<TLContact> contacts ,TLVector<TLAbsUser> users ){
			this.contacts = contacts; 
this.users = users; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            contacts = (TLVector<TLContact>)ObjectUtils.DeserializeVector<TLContact>(br);
users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(contacts,bw);
ObjectUtils.SerializeObject(users,bw);

        }
    }
}
