using System.IO;

namespace TeleSharp.TL.Updates
{
    [TLObject(630429265)]
    public class TLRequestGetDifference : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 630429265;
            }
        }

        public int Date { get; set; }

        public int Flags { get; set; }

        public int Pts { get; set; }

        public int? PtsTotalLimit { get; set; }

        public int Qts { get; set; }

        public Updates.TLAbsDifference Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Pts = br.ReadInt32();
            if ((Flags & 1) != 0)
                PtsTotalLimit = br.ReadInt32();
            else
                PtsTotalLimit = null;

            Date = br.ReadInt32();
            Qts = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Updates.TLAbsDifference)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(Pts);
            if ((Flags & 1) != 0)
                bw.Write(PtsTotalLimit.Value);
            bw.Write(Date);
            bw.Write(Qts);
        }
    }
}
