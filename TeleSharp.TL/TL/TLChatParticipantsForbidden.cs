using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-57668565)]
    public class TLChatParticipantsForbidden : TLAbsChatParticipants
    {
        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return -57668565;
            }
        }

        public int Flags { get; set; }

        public TLAbsChatParticipant SelfParticipant { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ChatId = br.ReadInt32();
            if ((Flags & 1) != 0)
                SelfParticipant = (TLAbsChatParticipant)ObjectUtils.DeserializeObject(br);
            else
                SelfParticipant = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(ChatId);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(SelfParticipant, bw);
        }
    }
}
