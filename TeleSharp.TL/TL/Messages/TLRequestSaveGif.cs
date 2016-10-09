using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(846868683)]
    public class TLRequestSaveGif : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 846868683;
            }
        }

                private TLAbsInputDocument id {get;set;}
        private bool unsave {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestSaveGif (TLAbsInputDocument id ,bool unsave ){
			this.id = id; 
this.unsave = unsave; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
unsave = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(id,bw);
BoolUtil.Serialize(unsave,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
