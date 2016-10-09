using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-732523960)]
    public class TLRequestSearch : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -732523960;
            }
        }

                private int flags {get;set;}
        private TLAbsInputPeer peer {get;set;}
        private string q {get;set;}
        private TLAbsMessagesFilter filter {get;set;}
        private int min_date {get;set;}
        private int max_date {get;set;}
        private int offset {get;set;}
        private int max_id {get;set;}
        private int limit {get;set;}
        public Messages.TLAbsMessages Response{ get; set;}

		
		public TLRequestSearch (TLAbsInputPeer peer ,string q ,TLAbsMessagesFilter filter ,int min_date ,int max_date ,int offset ,int max_id ,int limit ){
			this.peer = peer; 
this.q = q; 
this.filter = filter; 
this.min_date = min_date; 
this.max_date = max_date; 
this.offset = offset; 
this.max_id = max_id; 
this.limit = limit; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
q = StringUtil.Deserialize(br);
filter = (TLAbsMessagesFilter)ObjectUtils.DeserializeObject(br);
min_date = br.ReadInt32();
max_date = br.ReadInt32();
offset = br.ReadInt32();
max_id = br.ReadInt32();
limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);
ObjectUtils.SerializeObject(peer,bw);
StringUtil.Serialize(q,bw);
ObjectUtils.SerializeObject(filter,bw);
bw.Write(min_date);
bw.Write(max_date);
bw.Write(offset);
bw.Write(max_id);
bw.Write(limit);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);

		}
    }
}
