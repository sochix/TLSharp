using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(9203775)]
    public class TLUserStatusOffline : TLAbsUserStatus
    {
        public override int Constructor
        {
            get
            {
                return 9203775;
            }
        }

        public int was_online { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            was_online = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(was_online);

        }
    }
}
