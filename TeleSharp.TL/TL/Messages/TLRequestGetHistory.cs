using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-1347868602)]
    public class TLRequestGetHistory : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1347868602;
            }
        }

                private TLAbsInputPeer peer {get;set;}
        private int offset_id {get;set;}
        private int offset_date {get;set;}
        private int add_offset {get;set;}
        private int limit {get;set;}
        private int max_id {get;set;}
        private int min_id {get;set;}
        public Messages.TLAbsMessages Response{ get; set;}

		
		public TLRequestGetHistory (TLAbsInputPeer peer ,int offset_id ,int offset_date ,int add_offset ,int limit ,int max_id ,int min_id ){
			this.peer = peer; 
this.offset_id = offset_id; 
this.offset_date = offset_date; 
this.add_offset = add_offset; 
this.limit = limit; 
this.max_id = max_id; 
this.min_id = min_id; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
offset_id = br.ReadInt32();
offset_date = br.ReadInt32();
add_offset = br.ReadInt32();
limit = br.ReadInt32();
max_id = br.ReadInt32();
min_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer,bw);
bw.Write(offset_id);
bw.Write(offset_date);
bw.Write(add_offset);
bw.Write(limit);
bw.Write(max_id);
bw.Write(min_id);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);

		}
    }
}
