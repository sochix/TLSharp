using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(2066793382)]
    public class TLRequestInstallStickerSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2066793382;
            }
        }

                private TLAbsInputStickerSet stickerset {get;set;}
        private bool disabled {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestInstallStickerSet (TLAbsInputStickerSet stickerset ,bool disabled ){
			this.stickerset = stickerset; 
this.disabled = disabled; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
disabled = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(stickerset,bw);
BoolUtil.Serialize(disabled,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
