using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1040964988)]
    public class TLRequestUpdateUsername : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1040964988;
            }
        }

        public string username { get; set; }
        public TLAbsUser Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            username = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(username, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }
    }
}
