using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Account
{
    [TLObject(-313079300)]
    public class TLWebAuthorizations : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -313079300;
            }
        }

        public TLVector<TLWebAuthorization> Authorizations { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Authorizations = (TLVector<TLWebAuthorization>)ObjectUtils.DeserializeVector<TLWebAuthorization>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Authorizations, bw);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
