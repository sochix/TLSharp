using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(1799878989)]
    public class TLRequestGetDialogs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1799878989;
            }
        }

                private int offset_date {get;set;}
        private int offset_id {get;set;}
        private TLAbsInputPeer offset_peer {get;set;}
        private int limit {get;set;}
        public Messages.TLAbsDialogs Response{ get; set;}

		
		public TLRequestGetDialogs (int offset_date ,int offset_id ,TLAbsInputPeer offset_peer ,int limit ){
			this.offset_date = offset_date; 
this.offset_id = offset_id; 
this.offset_peer = offset_peer; 
this.limit = limit; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            offset_date = br.ReadInt32();
offset_id = br.ReadInt32();
offset_peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(offset_date);
bw.Write(offset_id);
ObjectUtils.SerializeObject(offset_peer,bw);
bw.Write(limit);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLAbsDialogs)ObjectUtils.DeserializeObject(br);

		}
    }
}
