using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(1056025023)]
    public class TLRequestResendCode : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1056025023;
            }
        }

        public string phone_number { get; set; }
        public string phone_code_hash { get; set; }
        public Auth.TLSentCode Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);
            phone_code_hash = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(phone_number, bw);
            StringUtil.Serialize(phone_code_hash, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Auth.TLSentCode)ObjectUtils.DeserializeObject(br);

        }
    }
}
