using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1425052898)]
    public class TLUpdatePhoneCall : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1425052898;
            }
        }

        public TLAbsPhoneCall phone_call { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_call = (TLAbsPhoneCall)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(phone_call, bw);

        }
    }
}
