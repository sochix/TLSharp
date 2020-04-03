using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1893427255)]
    public class TLUpdateChannelAvailableMessages : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1893427255;
            }
        }

        public int ChannelId { get; set; }
        public int AvailableMinId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChannelId = br.ReadInt32();
            AvailableMinId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChannelId);
            bw.Write(AvailableMinId);

        }
    }
}
