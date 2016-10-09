using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-599447467)]
    public class TLRequestEditChatTitle : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -599447467;
            }
        }

                private int chat_id {get;set;}
        private string title {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestEditChatTitle (int chat_id ,string title ){
			this.chat_id = chat_id; 
this.title = title; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
title = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(chat_id);
StringUtil.Serialize(title,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
