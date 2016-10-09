using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
	[TLObject(-1374118561)]
    public class TLRequestReportPeer : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1374118561;
            }
        }

                private TLAbsInputPeer peer {get;set;}
        private TLAbsReportReason reason {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestReportPeer (TLAbsInputPeer peer ,TLAbsReportReason reason ){
			this.peer = peer; 
this.reason = reason; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
reason = (TLAbsReportReason)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer,bw);
ObjectUtils.SerializeObject(reason,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
