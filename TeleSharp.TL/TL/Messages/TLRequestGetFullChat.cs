using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(998448230)]
    public class TLRequestGetFullChat : TLMethod
    {
        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return 998448230;
            }
        }

        public Messages.TLChatFull Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLChatFull)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
        }
    }
}
