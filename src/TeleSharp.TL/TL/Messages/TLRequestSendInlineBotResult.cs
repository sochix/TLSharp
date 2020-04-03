using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(570955184)]
    public class TLRequestSendInlineBotResult : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 570955184;
            }
        }

        public int Flags { get; set; }
        public bool Silent { get; set; }
        public bool Background { get; set; }
        public bool ClearDraft { get; set; }
        public bool HideVia { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public int? ReplyToMsgId { get; set; }
        public long RandomId { get; set; }
        public long QueryId { get; set; }
        public string Id { get; set; }
        public int? ScheduleDate { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Silent = (Flags & 32) != 0;
            Background = (Flags & 64) != 0;
            ClearDraft = (Flags & 128) != 0;
            HideVia = (Flags & 2048) != 0;
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                ReplyToMsgId = br.ReadInt32();
            else
                ReplyToMsgId = null;

            RandomId = br.ReadInt64();
            QueryId = br.ReadInt64();
            Id = StringUtil.Deserialize(br);
            if ((Flags & 1024) != 0)
                ScheduleDate = br.ReadInt32();
            else
                ScheduleDate = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);




            ObjectUtils.SerializeObject(Peer, bw);
            if ((Flags & 1) != 0)
                bw.Write(ReplyToMsgId.Value);
            bw.Write(RandomId);
            bw.Write(QueryId);
            StringUtil.Serialize(Id, bw);
            if ((Flags & 1024) != 0)
                bw.Write(ScheduleDate.Value);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
