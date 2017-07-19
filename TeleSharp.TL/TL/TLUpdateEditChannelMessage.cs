using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(457133559)]
    public class TLUpdateEditChannelMessage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 457133559;
            }
        }

        public TLAbsMessage message { get; set; }
        public int pts { get; set; }
        public int pts_count { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            message = (TLAbsMessage)ObjectUtils.DeserializeObject(br);
            pts = br.ReadInt32();
            pts_count = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(message, bw);
            bw.Write(pts);
            bw.Write(pts_count);

        }
    }
}
