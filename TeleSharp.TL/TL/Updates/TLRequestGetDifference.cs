using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
	[TLObject(168039573)]
    public class TLRequestGetDifference : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 168039573;
            }
        }

                private int pts {get;set;}
        private int date {get;set;}
        private int qts {get;set;}
        public Updates.TLAbsDifference Response{ get; set;}

		
		public TLRequestGetDifference (int pts ,int date ,int qts ){
			this.pts = pts; 
this.date = date; 
this.qts = qts; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            pts = br.ReadInt32();
date = br.ReadInt32();
qts = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(pts);
bw.Write(date);
bw.Write(qts);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Updates.TLAbsDifference)ObjectUtils.DeserializeObject(br);

		}
    }
}
