using System.IO;
namespace TeleSharp.TL
{
    [TLObject(995769920)]
    public class TLChannelAdminLogEvent : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 995769920;
            }
        }

        public long id { get; set; }
        public int date { get; set; }
        public int user_id { get; set; }
        public TLAbsChannelAdminLogEventAction action { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            date = br.ReadInt32();
            user_id = br.ReadInt32();
            action = (TLAbsChannelAdminLogEventAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(date);
            bw.Write(user_id);
            ObjectUtils.SerializeObject(action, bw);

        }
    }
}
