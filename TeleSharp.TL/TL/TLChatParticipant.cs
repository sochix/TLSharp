using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-925415106)]
    public class TLChatParticipant : TLAbsChatParticipant
    {
        public override int Constructor
        {
            get
            {
                return -925415106;
            }
        }

        public int Date { get; set; }

        public int InviterId { get; set; }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            InviterId = br.ReadInt32();
            Date = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            bw.Write(InviterId);
            bw.Write(Date);
        }
    }
}
