using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-1494659324)]
    public class TLRequestGetBotCallbackAnswer : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1494659324;
            }
        }

                private TLAbsInputPeer peer {get;set;}
        private int msg_id {get;set;}
        private byte[] data {get;set;}
        public Messages.TLBotCallbackAnswer Response{ get; set;}

		
		public TLRequestGetBotCallbackAnswer (TLAbsInputPeer peer ,int msg_id ,byte[] data ){
			this.peer = peer; 
this.msg_id = msg_id; 
this.data = data; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
msg_id = br.ReadInt32();
data = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer,bw);
bw.Write(msg_id);
BytesUtil.Serialize(data,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLBotCallbackAnswer)ObjectUtils.DeserializeObject(br);

		}
    }
}
