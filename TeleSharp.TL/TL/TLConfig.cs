using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-918482040)]
    public class TLConfig : TLObject
    {

		
        public override int Constructor
        {
            get
            {
                return -918482040;
            }
        }

             public int date {get;set;}
     public int expires {get;set;}
     public bool test_mode {get;set;}
     public int this_dc {get;set;}
     public TLVector<TLDcOption> dc_options {get;set;}
     public int chat_size_max {get;set;}
     public int megagroup_size_max {get;set;}
     public int forwarded_count_max {get;set;}
     public int online_update_period_ms {get;set;}
     public int offline_blur_timeout_ms {get;set;}
     public int offline_idle_timeout_ms {get;set;}
     public int online_cloud_timeout_ms {get;set;}
     public int notify_cloud_delay_ms {get;set;}
     public int notify_default_delay_ms {get;set;}
     public int chat_big_size {get;set;}
     public int push_chat_period_ms {get;set;}
     public int push_chat_limit {get;set;}
     public int saved_gifs_limit {get;set;}
     public int edit_time_limit {get;set;}
     public int rating_e_decay {get;set;}
     public TLVector<TLDisabledFeature> disabled_features {get;set;}

		public TLConfig (){}
		public TLConfig (int date ,int expires ,bool test_mode ,int this_dc ,TLVector<TLDcOption> dc_options ,int chat_size_max ,int megagroup_size_max ,int forwarded_count_max ,int online_update_period_ms ,int offline_blur_timeout_ms ,int offline_idle_timeout_ms ,int online_cloud_timeout_ms ,int notify_cloud_delay_ms ,int notify_default_delay_ms ,int chat_big_size ,int push_chat_period_ms ,int push_chat_limit ,int saved_gifs_limit ,int edit_time_limit ,int rating_e_decay ,TLVector<TLDisabledFeature> disabled_features ){
			this.date = date; 
this.expires = expires; 
this.test_mode = test_mode; 
this.this_dc = this_dc; 
this.dc_options = dc_options; 
this.chat_size_max = chat_size_max; 
this.megagroup_size_max = megagroup_size_max; 
this.forwarded_count_max = forwarded_count_max; 
this.online_update_period_ms = online_update_period_ms; 
this.offline_blur_timeout_ms = offline_blur_timeout_ms; 
this.offline_idle_timeout_ms = offline_idle_timeout_ms; 
this.online_cloud_timeout_ms = online_cloud_timeout_ms; 
this.notify_cloud_delay_ms = notify_cloud_delay_ms; 
this.notify_default_delay_ms = notify_default_delay_ms; 
this.chat_big_size = chat_big_size; 
this.push_chat_period_ms = push_chat_period_ms; 
this.push_chat_limit = push_chat_limit; 
this.saved_gifs_limit = saved_gifs_limit; 
this.edit_time_limit = edit_time_limit; 
this.rating_e_decay = rating_e_decay; 
this.disabled_features = disabled_features; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            date = br.ReadInt32();
expires = br.ReadInt32();
test_mode = BoolUtil.Deserialize(br);
this_dc = br.ReadInt32();
dc_options = (TLVector<TLDcOption>)ObjectUtils.DeserializeVector<TLDcOption>(br);
chat_size_max = br.ReadInt32();
megagroup_size_max = br.ReadInt32();
forwarded_count_max = br.ReadInt32();
online_update_period_ms = br.ReadInt32();
offline_blur_timeout_ms = br.ReadInt32();
offline_idle_timeout_ms = br.ReadInt32();
online_cloud_timeout_ms = br.ReadInt32();
notify_cloud_delay_ms = br.ReadInt32();
notify_default_delay_ms = br.ReadInt32();
chat_big_size = br.ReadInt32();
push_chat_period_ms = br.ReadInt32();
push_chat_limit = br.ReadInt32();
saved_gifs_limit = br.ReadInt32();
edit_time_limit = br.ReadInt32();
rating_e_decay = br.ReadInt32();
disabled_features = (TLVector<TLDisabledFeature>)ObjectUtils.DeserializeVector<TLDisabledFeature>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(date);
bw.Write(expires);
BoolUtil.Serialize(test_mode,bw);
bw.Write(this_dc);
ObjectUtils.SerializeObject(dc_options,bw);
bw.Write(chat_size_max);
bw.Write(megagroup_size_max);
bw.Write(forwarded_count_max);
bw.Write(online_update_period_ms);
bw.Write(offline_blur_timeout_ms);
bw.Write(offline_idle_timeout_ms);
bw.Write(online_cloud_timeout_ms);
bw.Write(notify_cloud_delay_ms);
bw.Write(notify_default_delay_ms);
bw.Write(chat_big_size);
bw.Write(push_chat_period_ms);
bw.Write(push_chat_limit);
bw.Write(saved_gifs_limit);
bw.Write(edit_time_limit);
bw.Write(rating_e_decay);
ObjectUtils.SerializeObject(disabled_features,bw);

        }
    }
}
