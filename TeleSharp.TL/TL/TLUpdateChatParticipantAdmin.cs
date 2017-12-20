using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1232070311)]
    public class TLUpdateChatParticipantAdmin : TLAbsUpdate
    {
        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return -1232070311;
            }
        }

        public bool IsAdmin { get; set; }

        public int UserId { get; set; }

        public int Version { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt32();
            UserId = br.ReadInt32();
            IsAdmin = BoolUtil.Deserialize(br);
            Version = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
            bw.Write(UserId);
            BoolUtil.Serialize(IsAdmin, bw);
            bw.Write(Version);
        }
    }
}
