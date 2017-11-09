using System.IO;
namespace TeleSharp.TL.Channels
{
    [TLObject(870184064)]
    public class TLRequestGetAdminLog : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 870184064;
            }
        }

        public int flags { get; set; }
        public TLAbsInputChannel channel { get; set; }
        public string q { get; set; }
        public TLChannelAdminLogEventsFilter events_filter { get; set; }
        public TLVector<TLAbsInputUser> admins { get; set; }
        public long max_id { get; set; }
        public long min_id { get; set; }
        public int limit { get; set; }
        public Channels.TLAdminLogResults Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = events_filter != null ? (flags | 1) : (flags & ~1);
            flags = admins != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            q = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                events_filter = (TLChannelAdminLogEventsFilter)ObjectUtils.DeserializeObject(br);
            else
                events_filter = null;

            if ((flags & 2) != 0)
                admins = (TLVector<TLAbsInputUser>)ObjectUtils.DeserializeVector<TLAbsInputUser>(br);
            else
                admins = null;

            max_id = br.ReadInt64();
            min_id = br.ReadInt64();
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            ObjectUtils.SerializeObject(channel, bw);
            StringUtil.Serialize(q, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(events_filter, bw);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(admins, bw);
            bw.Write(max_id);
            bw.Write(min_id);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Channels.TLAdminLogResults)ObjectUtils.DeserializeObject(br);

        }
    }
}
