using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-852477119)]
    public class TLStickerSet : TLObject
    {

		
        public override int Constructor
        {
            get
            {
                return -852477119;
            }
        }

             public int flags {get;set;}
     public bool installed {get;set;}
     public bool disabled {get;set;}
     public bool official {get;set;}
     public long id {get;set;}
     public long access_hash {get;set;}
     public string title {get;set;}
     public string short_name {get;set;}
     public int count {get;set;}
     public int hash {get;set;}

		public TLStickerSet (){}
		public TLStickerSet (bool installed ,bool disabled ,bool official ,long id ,long access_hash ,string title ,string short_name ,int count ,int hash ){
			this.installed = installed; 
this.disabled = disabled; 
this.official = official; 
this.id = id; 
this.access_hash = access_hash; 
this.title = title; 
this.short_name = short_name; 
this.count = count; 
this.hash = hash; 
	
		}
		public void ComputeFlags()
		{
			flags = 0;
flags = installed ? (flags | 1) : (flags & ~1);
flags = disabled ? (flags | 2) : (flags & ~2);
flags = official ? (flags | 4) : (flags & ~4);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
installed = (flags & 1) != 0;
disabled = (flags & 2) != 0;
official = (flags & 4) != 0;
id = br.ReadInt64();
access_hash = br.ReadInt64();
title = StringUtil.Deserialize(br);
short_name = StringUtil.Deserialize(br);
count = br.ReadInt32();
hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);



bw.Write(id);
bw.Write(access_hash);
StringUtil.Serialize(title,bw);
StringUtil.Serialize(short_name,bw);
bw.Write(count);
bw.Write(hash);

        }
    }
}
