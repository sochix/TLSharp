using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(258170395)]
    public class TLRequestGetInlineGameHighScores : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 258170395;
            }
        }

        public TLInputBotInlineMessageID Id { get; set; }

        public Messages.TLHighScores Response { get; set; }

        public TLAbsInputUser UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLHighScores)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
            ObjectUtils.SerializeObject(UserId, bw);
        }
    }
}
