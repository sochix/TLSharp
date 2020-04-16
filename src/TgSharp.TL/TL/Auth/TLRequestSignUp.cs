using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Auth
{
    [TLObject(-2131827673)]
    public class TLRequestSignUp : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2131827673;
            }
        }

        public string PhoneNumber { get; set; }
        public string PhoneCodeHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // manual edit: TLAbsAuthorization->TLAuthorization
        public Auth.TLAuthorization Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhoneNumber = StringUtil.Deserialize(br);
            PhoneCodeHash = StringUtil.Deserialize(br);
            FirstName = StringUtil.Deserialize(br);
            LastName = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(PhoneNumber, bw);
            StringUtil.Serialize(PhoneCodeHash, bw);
            StringUtil.Serialize(FirstName, bw);
            StringUtil.Serialize(LastName, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            // manual edit: TLAbsAuthorization->TLAuthorization
            Response = (Auth.TLAuthorization)ObjectUtils.DeserializeObject(br);
        }
    }
}
