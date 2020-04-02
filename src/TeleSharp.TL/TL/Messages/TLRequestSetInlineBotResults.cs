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

        public int Flags { get; set; }
        public bool Gallery { get; set; }
        public bool Private { get; set; }
        public long QueryId { get; set; }
        public TLVector<TLAbsInputBotInlineResult> Results { get; set; }
        public int CacheTime { get; set; }
        public string NextOffset { get; set; }
        public TLInlineBotSwitchPM SwitchPm { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Gallery ? (Flags | 1) : (Flags & ~1);
            Flags = Private ? (Flags | 2) : (Flags & ~2);
            Flags = NextOffset != null ? (Flags | 4) : (Flags & ~4);
            Flags = SwitchPm != null ? (Flags | 8) : (Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Gallery = (Flags & 1) != 0;
            Private = (Flags & 2) != 0;
            QueryId = br.ReadInt64();
            Results = (TLVector<TLAbsInputBotInlineResult>)ObjectUtils.DeserializeVector<TLAbsInputBotInlineResult>(br);
            CacheTime = br.ReadInt32();
            if ((Flags & 4) != 0)
                NextOffset = StringUtil.Deserialize(br);
            else
                NextOffset = null;

            if ((Flags & 8) != 0)
                SwitchPm = (TLInlineBotSwitchPM)ObjectUtils.DeserializeObject(br);
            else
                SwitchPm = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);


            bw.Write(QueryId);
            ObjectUtils.SerializeObject(Results, bw);
            bw.Write(CacheTime);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(NextOffset, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(SwitchPm, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
