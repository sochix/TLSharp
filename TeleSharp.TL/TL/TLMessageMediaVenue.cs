using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(2031269663)]
    public class TLMessageMediaVenue : TLAbsMessageMedia
    {

		
        public override int Constructor
        {
            get
            {
                return 2031269663;
            }
        }

             public TLAbsGeoPoint geo {get;set;}
     public string title {get;set;}
     public string address {get;set;}
     public string provider {get;set;}
     public string venue_id {get;set;}

		public TLMessageMediaVenue (){
			
		}
		public TLMessageMediaVenue (TLAbsGeoPoint geo ,string title ,string address ,string provider ,string venue_id ){
			this.geo = geo; 
this.title = title; 
this.address = address; 
this.provider = provider; 
this.venue_id = venue_id; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
title = StringUtil.Deserialize(br);
address = StringUtil.Deserialize(br);
provider = StringUtil.Deserialize(br);
venue_id = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(geo,bw);
StringUtil.Serialize(title,bw);
StringUtil.Serialize(address,bw);
StringUtil.Serialize(provider,bw);
StringUtil.Serialize(venue_id,bw);

        }
    }
}
