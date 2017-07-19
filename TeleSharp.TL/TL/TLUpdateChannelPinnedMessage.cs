using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1738988427)]
    public class TLUpdateChannelPinnedMessage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1738988427;
            }
        }

        public int channel_id { get; set; }
        public int id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel_id = br.ReadInt32();
            id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(channel_id);
            bw.Write(id);

        }
    }
}
