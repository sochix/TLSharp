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

        public int Flags { get; set; }
        public bool Gallery { get; set; }
        public long QueryId { get; set; }
        public string NextOffset { get; set; }
        public TLInlineBotSwitchPM SwitchPm { get; set; }
        public TLVector<TLAbsBotInlineResult> Results { get; set; }
        public int CacheTime { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Gallery ? (Flags | 1) : (Flags & ~1);
            Flags = NextOffset != null ? (Flags | 2) : (Flags & ~2);
            Flags = SwitchPm != null ? (Flags | 4) : (Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Gallery = (Flags & 1) != 0;
            QueryId = br.ReadInt64();
            if ((Flags & 2) != 0)
                NextOffset = StringUtil.Deserialize(br);
            else
                NextOffset = null;

            if ((Flags & 4) != 0)
                SwitchPm = (TLInlineBotSwitchPM)ObjectUtils.DeserializeObject(br);
            else
                SwitchPm = null;

            Results = (TLVector<TLAbsBotInlineResult>)ObjectUtils.DeserializeVector<TLAbsBotInlineResult>(br);
            CacheTime = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            bw.Write(QueryId);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(NextOffset, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(SwitchPm, bw);
            ObjectUtils.SerializeObject(Results, bw);
            bw.Write(CacheTime);

        }
    }
}
