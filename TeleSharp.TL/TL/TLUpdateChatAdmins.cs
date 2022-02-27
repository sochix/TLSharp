using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1855224129)]
    public class TLUpdateChatAdmins : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1855224129;
            }
        }

        public long ChatId { get; set; }
        public bool Enabled { get; set; }
        public int Version { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt64();
            Enabled = BoolUtil.Deserialize(br);
            Version = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
            BoolUtil.Serialize(Enabled, bw);
            bw.Write(Version);

        }
    }
}
