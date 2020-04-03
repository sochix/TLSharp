using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1880182943)]
    public class TLRequestSendVerifyEmailCode : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1880182943;
            }
        }

        public string Email { get; set; }
        public Account.TLSentEmailCode Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Email = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Email, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Account.TLSentEmailCode)ObjectUtils.DeserializeObject(br);

        }
    }
}
