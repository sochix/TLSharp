using System.IO;

namespace TeleSharp.TL
{
    [TLObject(120753115)]
    public class TLChatForbidden : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return 120753115;
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
            Title = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            StringUtil.Serialize(Title, bw);
        }
    }
}
