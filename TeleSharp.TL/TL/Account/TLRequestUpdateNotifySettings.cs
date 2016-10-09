using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
	[TLObject(-2067899501)]
    public class TLRequestUpdateNotifySettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2067899501;
            }
        }

                private TLAbsInputNotifyPeer peer {get;set;}
        private TLInputPeerNotifySettings settings {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestUpdateNotifySettings (TLAbsInputNotifyPeer peer ,TLInputPeerNotifySettings settings ){
			this.peer = peer; 
this.settings = settings; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputNotifyPeer)ObjectUtils.DeserializeObject(br);
settings = (TLInputPeerNotifySettings)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer,bw);
ObjectUtils.SerializeObject(settings,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
