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

        public int Flags { get; set; }
        public bool ExcludeNewMessages { get; set; }
        public TLVector<TLMessageRange> Ranges { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = ExcludeNewMessages ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ExcludeNewMessages = (Flags & 2) != 0;
            Ranges = (TLVector<TLMessageRange>)ObjectUtils.DeserializeVector<TLMessageRange>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Ranges, bw);

        }
    }
}
