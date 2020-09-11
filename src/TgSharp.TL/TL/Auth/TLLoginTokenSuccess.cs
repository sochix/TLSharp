using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Auth
{
    [TLObject(957176926)]
    public class TLLoginTokenSuccess : TLAbsLoginToken
    {
        public override int Constructor
        {
            get
            {
                return 957176926;
            }
        }

        public Auth.TLAbsAuthorization Authorization { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Authorization = (Auth.TLAbsAuthorization)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Authorization, bw);
        }
    }
}
