using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1444503762)]
    public class TLRequestEditChatAdmin : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1444503762;
            }
        }

        public long ChatId { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public bool IsAdmin { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt64();
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            IsAdmin = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
            ObjectUtils.SerializeObject(UserId, bw);
            BoolUtil.Serialize(IsAdmin, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
