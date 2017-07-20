using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
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

        public int flags { get; set; }
        public int pts { get; set; }
        public int? pts_total_limit { get; set; }
        public int date { get; set; }
        public int qts { get; set; }
        public Updates.TLAbsDifference Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = pts_total_limit != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            pts = br.ReadInt32();
            if ((flags & 1) != 0)
                pts_total_limit = br.ReadInt32();
            else
                pts_total_limit = null;

            date = br.ReadInt32();
            qts = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(pts);
            if ((flags & 1) != 0)
                bw.Write(pts_total_limit.Value);
            bw.Write(date);
            bw.Write(qts);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Updates.TLAbsDifference)ObjectUtils.DeserializeObject(br);

        }
    }
}
