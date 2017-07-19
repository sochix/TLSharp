using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(986597452)]
    public class TLLink : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 986597452;
            }
        }

        public TLAbsContactLink my_link { get; set; }
        public TLAbsContactLink foreign_link { get; set; }
        public TLAbsUser user { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            my_link = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);
            foreign_link = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);
            user = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(my_link, bw);
            ObjectUtils.SerializeObject(foreign_link, bw);
            ObjectUtils.SerializeObject(user, bw);

        }
    }
}
