using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1232070311)]
    public class TLUpdateChatParticipantAdmin : TLAbsUpdate
    {
        public override int Constructor => -1232070311;

        public int chat_id { get; set; }
        public int user_id { get; set; }
        public bool is_admin { get; set; }
        public int version { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
            user_id = br.ReadInt32();
            is_admin = BoolUtil.Deserialize(br);
            version = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
            bw.Write(user_id);
            BoolUtil.Serialize(is_admin, bw);
            bw.Write(version);
        }
    }
}