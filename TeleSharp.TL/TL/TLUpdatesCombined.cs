using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1918567619)]
    public class TLUpdatesCombined : TLAbsUpdates
    {

		
        public override int Constructor
        {
            get
            {
                return 1918567619;
            }
        }

             public TLVector<TLAbsUpdate> updates {get;set;}
     public TLVector<TLAbsUser> users {get;set;}
     public TLVector<TLAbsChat> chats {get;set;}
     public int date {get;set;}
     public int seq_start {get;set;}
     public int seq {get;set;}

		public TLUpdatesCombined (TLVector<TLAbsUpdate> updates ,TLVector<TLAbsUser> users ,TLVector<TLAbsChat> chats ,int date ,int seq_start ,int seq ){
			this.updates = updates; 
this.users = users; 
this.chats = chats; 
this.date = date; 
this.seq_start = seq_start; 
this.seq = seq; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            updates = (TLVector<TLAbsUpdate>)ObjectUtils.DeserializeVector<TLAbsUpdate>(br);
users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
date = br.ReadInt32();
seq_start = br.ReadInt32();
seq = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(updates,bw);
ObjectUtils.SerializeObject(users,bw);
ObjectUtils.SerializeObject(chats,bw);
bw.Write(date);
bw.Write(seq_start);
bw.Write(seq);

        }
    }
}
