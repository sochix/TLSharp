using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-370660328)]
    public class TLChannelAdminLogEventActionUpdatePinned : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return -370660328;
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
