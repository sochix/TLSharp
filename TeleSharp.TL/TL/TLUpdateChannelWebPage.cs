using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1081547008)]
    public class TLUpdateChannelWebPage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1081547008;
            }
        }

        public int channel_id { get; set; }
        public TLAbsWebPage webpage { get; set; }
        public int pts { get; set; }
        public int pts_count { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel_id = br.ReadInt32();
            webpage = (TLAbsWebPage)ObjectUtils.DeserializeObject(br);
            pts = br.ReadInt32();
            pts_count = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(channel_id);
            ObjectUtils.SerializeObject(webpage, bw);
            bw.Write(pts);
            bw.Write(pts_count);

        }
    }
}
