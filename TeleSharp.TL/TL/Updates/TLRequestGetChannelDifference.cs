using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(51854712)]
    public class TLRequestGetChannelDifference : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 51854712;
            }
        }

        public int flags { get; set; }
        public bool force { get; set; }
        public TLAbsInputChannel channel { get; set; }
        public TLAbsChannelMessagesFilter filter { get; set; }
        public int pts { get; set; }
        public int limit { get; set; }
        public Updates.TLAbsChannelDifference Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = force ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            force = (flags & 1) != 0;
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            filter = (TLAbsChannelMessagesFilter)ObjectUtils.DeserializeObject(br);
            pts = br.ReadInt32();
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(filter, bw);
            bw.Write(pts);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Updates.TLAbsChannelDifference)ObjectUtils.DeserializeObject(br);

        }
    }
}
