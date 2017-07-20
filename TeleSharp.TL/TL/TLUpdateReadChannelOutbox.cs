using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(634833351)]
    public class TLUpdateReadChannelOutbox : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 634833351;
            }
        }

        public int channel_id { get; set; }
        public int max_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel_id = br.ReadInt32();
            max_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(channel_id);
            bw.Write(max_id);

        }
    }
}
