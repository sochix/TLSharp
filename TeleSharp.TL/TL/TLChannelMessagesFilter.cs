using System.IO;

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

        public bool ExcludeNewMessages { get; set; }

        public int Flags { get; set; }

        public TLVector<TLMessageRange> Ranges { get; set; }

        public void ComputeFlags()
        {
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
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Ranges, bw);
        }
    }
}
