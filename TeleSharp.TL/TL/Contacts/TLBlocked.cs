using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(471043349)]
    public class TLBlocked : TLAbsBlocked
    {
        public override int Constructor
        {
            get
            {
                return 471043349;
            }
        }

        public TLVector<TLContactBlocked> blocked { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            blocked = (TLVector<TLContactBlocked>)ObjectUtils.DeserializeVector<TLContactBlocked>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(blocked, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
