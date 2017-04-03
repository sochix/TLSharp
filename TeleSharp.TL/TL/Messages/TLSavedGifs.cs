using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(772213157)]
    public class TLSavedGifs : TLAbsSavedGifs
    {
        public override int Constructor
        {
            get
            {
                return 772213157;
            }
        }

             public int hash {get;set;}
     public TLVector<TLAbsDocument> gifs {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt32();
gifs = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(hash);
ObjectUtils.SerializeObject(gifs,bw);

        }
    }
}
