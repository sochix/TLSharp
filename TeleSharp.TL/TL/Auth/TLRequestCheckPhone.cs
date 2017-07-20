using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(1877286395)]
    public class TLRequestCheckPhone : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1877286395;
            }
        }

        public string phone_number { get; set; }
        public Auth.TLCheckedPhone Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(phone_number, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Auth.TLCheckedPhone)ObjectUtils.DeserializeObject(br);

        }
    }
}
