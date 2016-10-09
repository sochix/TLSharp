using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(998448230)]
    public class TLRequestGetFullChat : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 998448230;
            }
        }

                private int chat_id {get;set;}
        public Messages.TLChatFull Response{ get; set;}

		
		public TLRequestGetFullChat (int chat_id ){
			this.chat_id = chat_id; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(chat_id);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLChatFull)ObjectUtils.DeserializeObject(br);

		}
    }
}
