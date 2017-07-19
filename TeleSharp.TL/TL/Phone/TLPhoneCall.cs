using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(-326966976)]
    public class TLPhoneCall : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -326966976;
            }
        }

        public TLAbsPhoneCall phone_call { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_call = (TLAbsPhoneCall)ObjectUtils.DeserializeObject(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(phone_call, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
