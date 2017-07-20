using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-882895228)]
    public class TLConfig : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -882895228;
            }
        }

        public int flags { get; set; }
        public bool phonecalls_enabled { get; set; }
        public int date { get; set; }
        public int expires { get; set; }
        public bool test_mode { get; set; }
        public int this_dc { get; set; }
        public TLVector<TLDcOption> dc_options { get; set; }
        public int chat_size_max { get; set; }
        public int megagroup_size_max { get; set; }
        public int forwarded_count_max { get; set; }
        public int online_update_period_ms { get; set; }
        public int offline_blur_timeout_ms { get; set; }
        public int offline_idle_timeout_ms { get; set; }
        public int online_cloud_timeout_ms { get; set; }
        public int notify_cloud_delay_ms { get; set; }
        public int notify_default_delay_ms { get; set; }
        public int chat_big_size { get; set; }
        public int push_chat_period_ms { get; set; }
        public int push_chat_limit { get; set; }
        public int saved_gifs_limit { get; set; }
        public int edit_time_limit { get; set; }
        public int rating_e_decay { get; set; }
        public int stickers_recent_limit { get; set; }
        public int? tmp_sessions { get; set; }
        public int pinned_dialogs_count_max { get; set; }
        public int call_receive_timeout_ms { get; set; }
        public int call_ring_timeout_ms { get; set; }
        public int call_connect_timeout_ms { get; set; }
        public int call_packet_timeout_ms { get; set; }
        public string me_url_prefix { get; set; }
        public TLVector<TLDisabledFeature> disabled_features { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = phonecalls_enabled ? (flags | 2) : (flags & ~2);
            flags = tmp_sessions != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            phonecalls_enabled = (flags & 2) != 0;
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
            stickers_recent_limit = br.ReadInt32();
            if ((flags & 1) != 0)
                tmp_sessions = br.ReadInt32();
            else
                tmp_sessions = null;

            pinned_dialogs_count_max = br.ReadInt32();
            call_receive_timeout_ms = br.ReadInt32();
            call_ring_timeout_ms = br.ReadInt32();
            call_connect_timeout_ms = br.ReadInt32();
            call_packet_timeout_ms = br.ReadInt32();
            me_url_prefix = StringUtil.Deserialize(br);
            disabled_features = (TLVector<TLDisabledFeature>)ObjectUtils.DeserializeVector<TLDisabledFeature>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(date);
            bw.Write(expires);
            BoolUtil.Serialize(test_mode, bw);
            bw.Write(this_dc);
            ObjectUtils.SerializeObject(dc_options, bw);
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
            bw.Write(stickers_recent_limit);
            if ((flags & 1) != 0)
                bw.Write(tmp_sessions.Value);
            bw.Write(pinned_dialogs_count_max);
            bw.Write(call_receive_timeout_ms);
            bw.Write(call_ring_timeout_ms);
            bw.Write(call_connect_timeout_ms);
            bw.Write(call_packet_timeout_ms);
            StringUtil.Serialize(me_url_prefix, bw);
            ObjectUtils.SerializeObject(disabled_features, bw);

        }
    }
}
