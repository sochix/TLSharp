using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(2106086025)]
    public class TLRequestExportChatInvite : TLMethod
    {
        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return 2106086025;
            }
        }

        public TLAbsExportedChatInvite Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsExportedChatInvite)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
        }
    }
}
