using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(1888354709)]
    public class TLRequestForwardMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1888354709;
            }
        }

                private int flags {get;set;}
        private bool silent {get;set;}
        private bool background {get;set;}
        private TLAbsInputPeer from_peer {get;set;}
        private TLVector<int> id {get;set;}
        private TLVector<long> random_id {get;set;}
        private TLAbsInputPeer to_peer {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestForwardMessages (bool silent ,bool background ,TLAbsInputPeer from_peer ,TLVector<int> id ,TLVector<long> random_id ,TLAbsInputPeer to_peer ){
			this.silent = silent; 
this.background = background; 
this.from_peer = from_peer; 
this.id = id; 
this.random_id = random_id; 
this.to_peer = to_peer; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;
flags = silent ? (flags | 32) : (flags & ~32);
flags = background ? (flags | 64) : (flags & ~64);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
silent = (flags & 32) != 0;
background = (flags & 64) != 0;
from_peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
random_id = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
to_peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);


ObjectUtils.SerializeObject(from_peer,bw);
ObjectUtils.SerializeObject(id,bw);
ObjectUtils.SerializeObject(random_id,bw);
ObjectUtils.SerializeObject(to_peer,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
