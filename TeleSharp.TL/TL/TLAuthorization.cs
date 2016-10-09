using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(2079516406)]
    public class TLAuthorization : TLObject
    {

		
        public override int Constructor
        {
            get
            {
                return 2079516406;
            }
        }

             public long hash {get;set;}
     public int flags {get;set;}
     public string device_model {get;set;}
     public string platform {get;set;}
     public string system_version {get;set;}
     public int api_id {get;set;}
     public string app_name {get;set;}
     public string app_version {get;set;}
     public int date_created {get;set;}
     public int date_active {get;set;}
     public string ip {get;set;}
     public string country {get;set;}
     public string region {get;set;}

		public TLAuthorization (){
			
		}
		public TLAuthorization (long hash ,string device_model ,string platform ,string system_version ,int api_id ,string app_name ,string app_version ,int date_created ,int date_active ,string ip ,string country ,string region ){
			this.hash = hash; 
this.device_model = device_model; 
this.platform = platform; 
this.system_version = system_version; 
this.api_id = api_id; 
this.app_name = app_name; 
this.app_version = app_version; 
this.date_created = date_created; 
this.date_active = date_active; 
this.ip = ip; 
this.country = country; 
this.region = region; 
	
		}
		public void ComputeFlags()
		{
			flags = 0;

		}

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt64();
flags = br.ReadInt32();
device_model = StringUtil.Deserialize(br);
platform = StringUtil.Deserialize(br);
system_version = StringUtil.Deserialize(br);
api_id = br.ReadInt32();
app_name = StringUtil.Deserialize(br);
app_version = StringUtil.Deserialize(br);
date_created = br.ReadInt32();
date_active = br.ReadInt32();
ip = StringUtil.Deserialize(br);
country = StringUtil.Deserialize(br);
region = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);
bw.Write(hash);
StringUtil.Serialize(device_model,bw);
StringUtil.Serialize(platform,bw);
StringUtil.Serialize(system_version,bw);
bw.Write(api_id);
StringUtil.Serialize(app_name,bw);
StringUtil.Serialize(app_version,bw);
bw.Write(date_created);
bw.Write(date_active);
StringUtil.Serialize(ip,bw);
StringUtil.Serialize(country,bw);
StringUtil.Serialize(region,bw);

        }
    }
}
