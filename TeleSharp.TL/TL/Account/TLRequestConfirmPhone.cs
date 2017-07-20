using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1596029123)]
    public class TLRequestConfirmPhone : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1596029123;
            }
        }

        public string phone_code_hash { get; set; }
        public string phone_code { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_code_hash = StringUtil.Deserialize(br);
            phone_code = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(phone_code_hash, bw);
            StringUtil.Serialize(phone_code, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
