using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(-667062079)]
    public class TLRequestClearSavedInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -667062079;
            }
        }

        public int flags { get; set; }
        public bool credentials { get; set; }
        public bool info { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = credentials ? (flags | 1) : (flags & ~1);
            flags = info ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            credentials = (flags & 1) != 0;
            info = (flags & 2) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);



        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
