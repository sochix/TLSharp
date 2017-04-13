using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2122045747)]
    public class TLPeerSettings : TLObject
    {
        public override int Constructor => -2122045747;

        public int flags { get; set; }
        public bool report_spam { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = report_spam ? flags | 1 : flags & ~1;
        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            report_spam = (flags & 1) != 0;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
        }
    }
}