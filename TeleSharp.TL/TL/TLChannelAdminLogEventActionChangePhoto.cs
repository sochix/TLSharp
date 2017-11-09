using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1204857405)]
    public class TLChannelAdminLogEventActionChangePhoto : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return -1204857405;
            }
        }

        public TLAbsChatPhoto prev_photo { get; set; }
        public TLAbsChatPhoto new_photo { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            prev_photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);
            new_photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(prev_photo, bw);
            ObjectUtils.SerializeObject(new_photo, bw);

        }
    }
}
