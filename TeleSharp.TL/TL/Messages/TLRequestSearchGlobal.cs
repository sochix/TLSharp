using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-1640190800)]
    public class TLRequestSearchGlobal : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1640190800;
            }
        }

                private string q {get;set;}
        private int offset_date {get;set;}
        private TLAbsInputPeer offset_peer {get;set;}
        private int offset_id {get;set;}
        private int limit {get;set;}
        public Messages.TLAbsMessages Response{ get; set;}

		
		public TLRequestSearchGlobal (string q ,int offset_date ,TLAbsInputPeer offset_peer ,int offset_id ,int limit ){
			this.q = q; 
this.offset_date = offset_date; 
this.offset_peer = offset_peer; 
this.offset_id = offset_id; 
this.limit = limit; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            q = StringUtil.Deserialize(br);
offset_date = br.ReadInt32();
offset_peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
offset_id = br.ReadInt32();
limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(q,bw);
bw.Write(offset_date);
ObjectUtils.SerializeObject(offset_peer,bw);
bw.Write(offset_id);
bw.Write(limit);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);

		}
    }
}
