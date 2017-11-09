using System.IO;
namespace TeleSharp.TL
{
    [TLObject(125178264)]
    public class TLUpdateChatParticipants : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 125178264;
            }
        }

        public TLAbsChatParticipants participants { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            participants = (TLAbsChatParticipants)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(participants, bw);

        }
    }
}
