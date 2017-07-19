using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-346119674)]
    public class TLRequestSetInlineBotResults : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -346119674;
            }
        }

        public int flags { get; set; }
        public bool gallery { get; set; }
        public bool @private { get; set; }
        public long query_id { get; set; }
        public TLVector<TLAbsInputBotInlineResult> results { get; set; }
        public int cache_time { get; set; }
        public string next_offset { get; set; }
        public TLInlineBotSwitchPM switch_pm { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = gallery ? (flags | 1) : (flags & ~1);
            flags = @private ? (flags | 2) : (flags & ~2);
            flags = next_offset != null ? (flags | 4) : (flags & ~4);
            flags = switch_pm != null ? (flags | 8) : (flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            gallery = (flags & 1) != 0;
            @private = (flags & 2) != 0;
            query_id = br.ReadInt64();
            results = (TLVector<TLAbsInputBotInlineResult>)ObjectUtils.DeserializeVector<TLAbsInputBotInlineResult>(br);
            cache_time = br.ReadInt32();
            if ((flags & 4) != 0)
                next_offset = StringUtil.Deserialize(br);
            else
                next_offset = null;

            if ((flags & 8) != 0)
                switch_pm = (TLInlineBotSwitchPM)ObjectUtils.DeserializeObject(br);
            else
                switch_pm = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            bw.Write(query_id);
            ObjectUtils.SerializeObject(results, bw);
            bw.Write(cache_time);
            if ((flags & 4) != 0)
                StringUtil.Serialize(next_offset, bw);
            if ((flags & 8) != 0)
                ObjectUtils.SerializeObject(switch_pm, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
