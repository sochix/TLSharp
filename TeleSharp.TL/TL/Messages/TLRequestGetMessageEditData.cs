using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-39416522)]
    public class TLRequestGetMessageEditData : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -39416522;
            }
        }

                private TLAbsInputPeer peer {get;set;}
        private int id {get;set;}
        public Messages.TLMessageEditData Response{ get; set;}

		
		public TLRequestGetMessageEditData (TLAbsInputPeer peer ,int id ){
			this.peer = peer; 
this.id = id; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer,bw);
bw.Write(id);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLMessageEditData)ObjectUtils.DeserializeObject(br);

		}
    }
}
