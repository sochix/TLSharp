using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(469850889)]
    public class TLRequestDeleteHistory : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 469850889;
            }
        }

                private int flags {get;set;}
        private bool just_clear {get;set;}
        private TLAbsInputPeer peer {get;set;}
        private int max_id {get;set;}
        public Messages.TLAffectedHistory Response{ get; set;}

		
		public TLRequestDeleteHistory (bool just_clear ,TLAbsInputPeer peer ,int max_id ){
			this.just_clear = just_clear; 
this.peer = peer; 
this.max_id = max_id; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;
flags = just_clear ? (flags | 1) : (flags & ~1);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
just_clear = (flags & 1) != 0;
peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
max_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);

ObjectUtils.SerializeObject(peer,bw);
bw.Write(max_id);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLAffectedHistory)ObjectUtils.DeserializeObject(br);

		}
    }
}
