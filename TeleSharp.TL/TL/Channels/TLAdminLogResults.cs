using System.IO;
namespace TeleSharp.TL.Channels
{
    [TLObject(-309659827)]
    public class TLAdminLogResults : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -309659827;
            }
        }

        public TLVector<TLChannelAdminLogEvent> events { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            events = (TLVector<TLChannelAdminLogEvent>)ObjectUtils.DeserializeVector<TLChannelAdminLogEvent>(br);
            chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(events, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
