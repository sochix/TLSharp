using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(363051235)]
    public class TLRequestMigrateChat : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 363051235;
            }
        }

        public long ChatId { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
