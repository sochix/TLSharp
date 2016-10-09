using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
	[TLObject(-728224331)]
    public class TLRequestGetTopPeers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -728224331;
            }
        }

                private int flags {get;set;}
        private bool correspondents {get;set;}
        private bool bots_pm {get;set;}
        private bool bots_inline {get;set;}
        private bool groups {get;set;}
        private bool channels {get;set;}
        private int offset {get;set;}
        private int limit {get;set;}
        private int hash {get;set;}
        public Contacts.TLAbsTopPeers Response{ get; set;}

		
		public TLRequestGetTopPeers (bool correspondents ,bool bots_pm ,bool bots_inline ,bool groups ,bool channels ,int offset ,int limit ,int hash ){
			this.correspondents = correspondents; 
this.bots_pm = bots_pm; 
this.bots_inline = bots_inline; 
this.groups = groups; 
this.channels = channels; 
this.offset = offset; 
this.limit = limit; 
this.hash = hash; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;
flags = correspondents ? (flags | 1) : (flags & ~1);
flags = bots_pm ? (flags | 2) : (flags & ~2);
flags = bots_inline ? (flags | 4) : (flags & ~4);
flags = groups ? (flags | 1024) : (flags & ~1024);
flags = channels ? (flags | 32768) : (flags & ~32768);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
correspondents = (flags & 1) != 0;
bots_pm = (flags & 2) != 0;
bots_inline = (flags & 4) != 0;
groups = (flags & 1024) != 0;
channels = (flags & 32768) != 0;
offset = br.ReadInt32();
limit = br.ReadInt32();
hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);





bw.Write(offset);
bw.Write(limit);
bw.Write(hash);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Contacts.TLAbsTopPeers)ObjectUtils.DeserializeObject(br);

		}
    }
}
