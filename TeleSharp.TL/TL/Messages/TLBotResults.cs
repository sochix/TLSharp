using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-858565059)]
    public class TLBotResults : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -858565059;
            }
        }

        public int flags { get; set; }
        public bool gallery { get; set; }
        public long query_id { get; set; }
        public string next_offset { get; set; }
        public TLInlineBotSwitchPM switch_pm { get; set; }
        public TLVector<TLAbsBotInlineResult> results { get; set; }
        public int cache_time { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = gallery ? (flags | 1) : (flags & ~1);
            flags = next_offset != null ? (flags | 2) : (flags & ~2);
            flags = switch_pm != null ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            gallery = (flags & 1) != 0;
            query_id = br.ReadInt64();
            if ((flags & 2) != 0)
                next_offset = StringUtil.Deserialize(br);
            else
                next_offset = null;

            if ((flags & 4) != 0)
                switch_pm = (TLInlineBotSwitchPM)ObjectUtils.DeserializeObject(br);
            else
                switch_pm = null;

            results = (TLVector<TLAbsBotInlineResult>)ObjectUtils.DeserializeVector<TLAbsBotInlineResult>(br);
            cache_time = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(query_id);
            if ((flags & 2) != 0)
                StringUtil.Serialize(next_offset, bw);
            if ((flags & 4) != 0)
                ObjectUtils.SerializeObject(switch_pm, bw);
            ObjectUtils.SerializeObject(results, bw);
            bw.Write(cache_time);

        }
    }
}
