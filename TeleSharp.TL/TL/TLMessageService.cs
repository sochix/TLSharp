using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1642487306)]
    public class TLMessageService : TLAbsMessage
    {
        public TLAbsMessageAction Action { get; set; }

        public override int Constructor
        {
            get
            {
                return -1642487306;
            }
        }

        public int Date { get; set; }

        public int Flags { get; set; }

        public int? FromId { get; set; }

        public int Id { get; set; }

        public bool MediaUnread { get; set; }

        public bool Mentioned { get; set; }

        public bool Out { get; set; }

        public bool Post { get; set; }

        public int? ReplyToMsgId { get; set; }

        public bool Silent { get; set; }

        public TLAbsPeer ToId { get; set; }

        public void ComputeFlags()
        {
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
