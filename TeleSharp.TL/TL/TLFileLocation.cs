using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1406570614)]
    public class TLFileLocation : TLAbsFileLocation
    {
        public override int Constructor
        {
            get
            {
                return 1406570614;
            }
        }

        public int dc_id { get; set; }
        public long volume_id { get; set; }
        public int local_id { get; set; }
        public long secret { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            dc_id = br.ReadInt32();
            volume_id = br.ReadInt64();
            local_id = br.ReadInt32();
            secret = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(dc_id);
            bw.Write(volume_id);
            bw.Write(local_id);
            bw.Write(secret);

        }
    }
}
