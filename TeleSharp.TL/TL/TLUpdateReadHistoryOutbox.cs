using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(791617983)]
    public class TLUpdateReadHistoryOutbox : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 791617983;
            }
        }

        public TLAbsPeer peer { get; set; }
        public int max_id { get; set; }
        public int pts { get; set; }
        public int pts_count { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            max_id = br.ReadInt32();
            pts = br.ReadInt32();
            pts_count = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(max_id);
            bw.Write(pts);
            bw.Write(pts_count);

        }
    }
}
