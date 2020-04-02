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

        public int Flags { get; set; }
        public bool Force { get; set; }
        public TLAbsInputChannel Channel { get; set; }
        public TLAbsChannelMessagesFilter Filter { get; set; }
        public int Pts { get; set; }
        public int Limit { get; set; }
        public Updates.TLAbsChannelDifference Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Force ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Force = (Flags & 1) != 0;
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Filter = (TLAbsChannelMessagesFilter)ObjectUtils.DeserializeObject(br);
            Pts = br.ReadInt32();
            Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Channel, bw);
            ObjectUtils.SerializeObject(Filter, bw);
            bw.Write(Pts);
            bw.Write(Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Updates.TLAbsChannelDifference)ObjectUtils.DeserializeObject(br);

        }
    }
}
