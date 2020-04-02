using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1642487306)]
    public class TLMessageService : TLAbsMessage
    {
        public override int Constructor
        {
            get
            {
                return -1642487306;
            }
        }

        public int Flags { get; set; }
        public bool Out { get; set; }
        public bool Mentioned { get; set; }
        public bool MediaUnread { get; set; }
        public bool Silent { get; set; }
        public bool Post { get; set; }
        public int Id { get; set; }
        public int? FromId { get; set; }
        public TLAbsPeer ToId { get; set; }
        public int? ReplyToMsgId { get; set; }
        public int Date { get; set; }
        public TLAbsMessageAction Action { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Out ? (Flags | 2) : (Flags & ~2);
            Flags = Mentioned ? (Flags | 16) : (Flags & ~16);
            Flags = MediaUnread ? (Flags | 32) : (Flags & ~32);
            Flags = Silent ? (Flags | 8192) : (Flags & ~8192);
            Flags = Post ? (Flags | 16384) : (Flags & ~16384);
            Flags = FromId != null ? (Flags | 256) : (Flags & ~256);
            Flags = ReplyToMsgId != null ? (Flags | 8) : (Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Out = (Flags & 2) != 0;
            Mentioned = (Flags & 16) != 0;
            MediaUnread = (Flags & 32) != 0;
            Silent = (Flags & 8192) != 0;
            Post = (Flags & 16384) != 0;
            Id = br.ReadInt32();
            if ((Flags & 256) != 0)
                FromId = br.ReadInt32();
            else
                FromId = null;

            ToId = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            if ((Flags & 8) != 0)
                ReplyToMsgId = br.ReadInt32();
            else
                ReplyToMsgId = null;

            Date = br.ReadInt32();
            Action = (TLAbsMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);





            bw.Write(Id);
            if ((Flags & 256) != 0)
                bw.Write(FromId.Value);
            ObjectUtils.SerializeObject(ToId, bw);
            if ((Flags & 8) != 0)
                bw.Write(ReplyToMsgId.Value);
            bw.Write(Date);
            ObjectUtils.SerializeObject(Action, bw);

        }
    }
}
