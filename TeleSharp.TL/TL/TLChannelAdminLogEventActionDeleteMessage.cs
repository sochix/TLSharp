using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1121994683)]
    public class TLChannelAdminLogEventActionDeleteMessage : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 1121994683;
            }
        }

        public TLAbsMessage Message { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Message = (TLAbsMessage)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Message, bw);
        }
    }
}
