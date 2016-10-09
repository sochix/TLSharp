using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(-934882771)]
    public class TLRequestExportMessageLink : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -934882771;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private int id {get;set;}
        public TLExportedMessageLink Response{ get; set;}

		
		public TLRequestExportMessageLink (TLAbsInputChannel channel ,int id ){
			this.channel = channel; 
this.id = id; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
bw.Write(id);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLExportedMessageLink)ObjectUtils.DeserializeObject(br);

		}
    }
}
