using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(618237842)]
    public class TLRequestGetParticipants : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 618237842;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private TLAbsChannelParticipantsFilter filter {get;set;}
        private int offset {get;set;}
        private int limit {get;set;}
        public Channels.TLChannelParticipants Response{ get; set;}

		
		public TLRequestGetParticipants (TLAbsInputChannel channel ,TLAbsChannelParticipantsFilter filter ,int offset ,int limit ){
			this.channel = channel; 
this.filter = filter; 
this.offset = offset; 
this.limit = limit; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
filter = (TLAbsChannelParticipantsFilter)ObjectUtils.DeserializeObject(br);
offset = br.ReadInt32();
limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
ObjectUtils.SerializeObject(filter,bw);
bw.Write(offset);
bw.Write(limit);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Channels.TLChannelParticipants)ObjectUtils.DeserializeObject(br);

		}
    }
}
