using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1663767815)]
    public class TLRequestGetPasswordSettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1663767815;
            }
        }

        public TLAbsInputCheckPasswordSRP Password { get; set; }
        public Account.TLPasswordSettings Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Password = (TLAbsInputCheckPasswordSRP)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Password, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Account.TLPasswordSettings)ObjectUtils.DeserializeObject(br);

        }
    }
}
