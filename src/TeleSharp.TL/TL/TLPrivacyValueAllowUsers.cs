using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1297858060)]
    public class TLPrivacyValueAllowUsers : TLAbsPrivacyRule
    {
        public override int Constructor
        {
            get
            {
                return 1297858060;
            }
        }

        public TLVector<int> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Users = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Users, bw);

        }
    }
}
