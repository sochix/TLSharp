using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-302170017)]
    public class TLAllStickers : TLAbsAllStickers
    {

		
        public override int Constructor
        {
            get
            {
                return -302170017;
            }
        }

             public int hash {get;set;}
     public TLVector<TLStickerSet> sets {get;set;}

		public TLAllStickers (){}
		public TLAllStickers (int hash ,TLVector<TLStickerSet> sets ){
			this.hash = hash; 
this.sets = sets; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt32();
sets = (TLVector<TLStickerSet>)ObjectUtils.DeserializeVector<TLStickerSet>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(hash);
ObjectUtils.SerializeObject(sets,bw);

        }
    }
}
