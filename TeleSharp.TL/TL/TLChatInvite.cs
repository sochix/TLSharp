using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1813406880)]
    public class TLChatInvite : TLAbsChatInvite
    {

		
        public override int Constructor
        {
            get
            {
                return -1813406880;
            }
        }

             public int flags {get;set;}
     public bool channel {get;set;}
     public bool broadcast {get;set;}
     public bool @public {get;set;}
     public bool megagroup {get;set;}
     public string title {get;set;}

		public TLChatInvite (){}
		public TLChatInvite (bool channel ,bool broadcast ,bool @public ,bool megagroup ,string title ){
			this.channel = channel; 
this.broadcast = broadcast; 
this.@public = @public; 
this.megagroup = megagroup; 
this.title = title; 
	
		}
		public void ComputeFlags()
		{
			flags = 0;
flags = channel ? (flags | 1) : (flags & ~1);
flags = broadcast ? (flags | 2) : (flags & ~2);
flags = @public ? (flags | 4) : (flags & ~4);
flags = megagroup ? (flags | 8) : (flags & ~8);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
channel = (flags & 1) != 0;
broadcast = (flags & 2) != 0;
@public = (flags & 4) != 0;
megagroup = (flags & 8) != 0;
title = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);




StringUtil.Serialize(title,bw);

        }
    }
}
