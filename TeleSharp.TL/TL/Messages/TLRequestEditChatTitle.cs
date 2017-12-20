using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-599447467)]
    public class TLRequestEditChatTitle : TLMethod
    {
        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return -599447467;
            }
        }

        public TLAbsUpdates Response { get; set; }

        public string Title { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt32();
            Title = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
            StringUtil.Serialize(Title, bw);
        }
    }
}
