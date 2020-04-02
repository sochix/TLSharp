using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1877932953)]
    public class TLInputPrivacyValueDisallowUsers : TLAbsInputPrivacyRule
    {
        public override int Constructor
        {
            get
            {
                return -1877932953;
            }
        }

        public TLVector<TLAbsInputUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Users = (TLVector<TLAbsInputUser>)ObjectUtils.DeserializeVector<TLAbsInputUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Users, bw);

        }
    }
}
