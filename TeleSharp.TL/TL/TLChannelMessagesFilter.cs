using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-847783593)]
    public class TLChannelMessagesFilter : TLAbsChannelMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return -847783593;
            }
        }

        public int flags { get; set; }
        public bool exclude_new_messages { get; set; }
        public TLVector<TLMessageRange> ranges { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = exclude_new_messages ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            exclude_new_messages = (flags & 2) != 0;
            ranges = (TLVector<TLMessageRange>)ObjectUtils.DeserializeVector<TLMessageRange>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(ranges, bw);

        }
    }
}
