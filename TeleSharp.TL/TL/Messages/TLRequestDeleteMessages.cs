using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-443640366)]
    public class TLRequestDeleteMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -443640366;
            }
        }

        public int flags { get; set; }
        public bool revoke { get; set; }
        public TLVector<int> id { get; set; }
        public Messages.TLAffectedMessages Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = revoke ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            revoke = (flags & 1) != 0;
            id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(id, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAffectedMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
