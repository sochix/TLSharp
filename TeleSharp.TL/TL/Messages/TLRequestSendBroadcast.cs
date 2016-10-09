using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-1082919718)]
    public class TLRequestSendBroadcast : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1082919718;
            }
        }

                private TLVector<TLAbsInputUser> contacts {get;set;}
        private TLVector<long> random_id {get;set;}
        private string message {get;set;}
        private TLAbsInputMedia media {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestSendBroadcast (TLVector<TLAbsInputUser> contacts ,TLVector<long> random_id ,string message ,TLAbsInputMedia media ){
			this.contacts = contacts; 
this.random_id = random_id; 
this.message = message; 
this.media = media; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            contacts = (TLVector<TLAbsInputUser>)ObjectUtils.DeserializeVector<TLAbsInputUser>(br);
random_id = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
message = StringUtil.Deserialize(br);
media = (TLAbsInputMedia)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(contacts,bw);
ObjectUtils.SerializeObject(random_id,bw);
StringUtil.Serialize(message,bw);
ObjectUtils.SerializeObject(media,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
