using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1889215493)]
    public class TLChannelAdminLogEventActionEditMessage : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 1889215493;
            }
        }

        public TLAbsMessage prev_message { get; set; }
        public TLAbsMessage new_message { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            prev_message = (TLAbsMessage)ObjectUtils.DeserializeObject(br);
            new_message = (TLAbsMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(prev_message, bw);
            ObjectUtils.SerializeObject(new_message, bw);

        }
    }
}
