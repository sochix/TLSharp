using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-855308010)]
    public class TLAuthorization : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -855308010;
            }
        }

        public int flags { get; set; }
        public int? tmp_sessions { get; set; }
        public TLAbsUser user { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = tmp_sessions != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            if ((flags & 1) != 0)
                tmp_sessions = br.ReadInt32();
            else
                tmp_sessions = null;

            user = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            if ((flags & 1) != 0)
                bw.Write(tmp_sessions.Value);
            ObjectUtils.SerializeObject(user, bw);

        }
    }
}
