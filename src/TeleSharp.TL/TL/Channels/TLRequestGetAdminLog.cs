using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
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

        public int Flags { get; set; }
        public TLAbsInputChannel Channel { get; set; }
        public string Q { get; set; }
        public TLChannelAdminLogEventsFilter EventsFilter { get; set; }
        public TLVector<TLAbsInputUser> Admins { get; set; }
        public long MaxId { get; set; }
        public long MinId { get; set; }
        public int Limit { get; set; }
        public Channels.TLAdminLogResults Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Q = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                EventsFilter = (TLChannelAdminLogEventsFilter)ObjectUtils.DeserializeObject(br);
            else
                EventsFilter = null;

            if ((Flags & 2) != 0)
                Admins = (TLVector<TLAbsInputUser>)ObjectUtils.DeserializeVector<TLAbsInputUser>(br);
            else
                Admins = null;

            MaxId = br.ReadInt64();
            MinId = br.ReadInt64();
            Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Channel, bw);
            StringUtil.Serialize(Q, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(EventsFilter, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(Admins, bw);
            bw.Write(MaxId);
            bw.Write(MinId);
            bw.Write(Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Channels.TLAdminLogResults)ObjectUtils.DeserializeObject(br);

        }
    }
}
