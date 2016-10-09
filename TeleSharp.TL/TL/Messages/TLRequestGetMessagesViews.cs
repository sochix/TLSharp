using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-993483427)]
    public class TLRequestGetMessagesViews : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -993483427;
            }
        }

                private TLAbsInputPeer peer {get;set;}
        private TLVector<int> id {get;set;}
        private bool increment {get;set;}
        public TLVector<int> Response{ get; set;}

		
		public TLRequestGetMessagesViews (TLAbsInputPeer peer ,TLVector<int> id ,bool increment ){
			this.peer = peer; 
this.id = id; 
this.increment = increment; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
increment = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer,bw);
ObjectUtils.SerializeObject(id,bw);
BoolUtil.Serialize(increment,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

		}
    }
}
