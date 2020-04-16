using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Auth
{
    [TLObject(1148485274)]
    public class TLAuthorizationSignUpRequired : TLAbsAuthorization
    {
        public override int Constructor
        {
            get
            {
                return 1148485274;
            }
        }

        public int Flags { get; set; }
        public Help.TLTermsOfService TermsOfService { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                TermsOfService = (Help.TLTermsOfService)ObjectUtils.DeserializeObject(br);
            else
                TermsOfService = null;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(TermsOfService, bw);
        }
    }
}
