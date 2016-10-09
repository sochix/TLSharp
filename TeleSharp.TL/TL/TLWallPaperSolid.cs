using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1662091044)]
    public class TLWallPaperSolid : TLAbsWallPaper
    {

		
        public override int Constructor
        {
            get
            {
                return 1662091044;
            }
        }

             public int id {get;set;}
     public string title {get;set;}
     public int bg_color {get;set;}
     public int color {get;set;}

		public TLWallPaperSolid (){
			
		}
		public TLWallPaperSolid (int id ,string title ,int bg_color ,int color ){
			this.id = id; 
this.title = title; 
this.bg_color = bg_color; 
this.color = color; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
title = StringUtil.Deserialize(br);
bg_color = br.ReadInt32();
color = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(id);
StringUtil.Serialize(title,bw);
bw.Write(bg_color);
bw.Write(color);

        }
    }
}
