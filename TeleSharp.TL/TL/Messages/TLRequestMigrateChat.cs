using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(363051235)]
    public class TLRequestMigrateChat : TLMethod
    {
        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return 363051235;
            }
        }

        public TLAbsUpdates Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
        }
    }
}
