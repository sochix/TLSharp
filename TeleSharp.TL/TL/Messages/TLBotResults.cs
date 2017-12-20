using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-858565059)]
    public class TLBotResults : TLObject
    {
        public int CacheTime { get; set; }

        public override int Constructor
        {
            get
            {
                return -858565059;
            }
        }

        public int Flags { get; set; }

        public bool Gallery { get; set; }

        public string NextOffset { get; set; }

        public long QueryId { get; set; }

        public TLVector<TLAbsBotInlineResult> Results { get; set; }

        public TLInlineBotSwitchPM SwitchPm { get; set; }

        public void ComputeFlags()
        {
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
