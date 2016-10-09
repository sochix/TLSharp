using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
	[TLObject(451113900)]
    public class TLRequestResetTopPeerRating : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 451113900;
            }
        }

                private TLAbsTopPeerCategory category {get;set;}
        private TLAbsInputPeer peer {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestResetTopPeerRating (TLAbsTopPeerCategory category ,TLAbsInputPeer peer ){
			this.category = category; 
this.peer = peer; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            category = (TLAbsTopPeerCategory)ObjectUtils.DeserializeObject(br);
peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(category,bw);
ObjectUtils.SerializeObject(peer,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
