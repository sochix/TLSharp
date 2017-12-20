using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1355435489)]
    public class TLPhoneCallDiscarded : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return 1355435489;
            }
        }

        public int? Duration { get; set; }

        public int Flags { get; set; }

        public long Id { get; set; }

        public bool NeedDebug { get; set; }

        public bool NeedRating { get; set; }

        public TLAbsPhoneCallDiscardReason Reason { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            NeedRating = (Flags & 4) != 0;
            NeedDebug = (Flags & 8) != 0;
            Id = br.ReadInt64();
            if ((Flags & 1) != 0)
                Reason = (TLAbsPhoneCallDiscardReason)ObjectUtils.DeserializeObject(br);
            else
                Reason = null;

            if ((Flags & 2) != 0)
                Duration = br.ReadInt32();
            else
                Duration = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            bw.Write(Id);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Reason, bw);
            if ((Flags & 2) != 0)
                bw.Write(Duration.Value);
        }
    }
}
