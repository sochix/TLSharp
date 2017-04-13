using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(258170395)]
    public class TLRequestGetInlineGameHighScores : TLMethod
    {
        public override int Constructor => 258170395;

        public TLInputBotInlineMessageID id { get; set; }
        public TLAbsInputUser user_id { get; set; }
        public TLHighScores Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLInputBotInlineMessageID) ObjectUtils.DeserializeObject(br);
            user_id = (TLAbsInputUser) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
            ObjectUtils.SerializeObject(user_id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLHighScores) ObjectUtils.DeserializeObject(br);
        }
    }
}