using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1151208273)]
    public class TLRequestGetTmpPassword : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1151208273;
            }
        }

        public TLAbsInputCheckPasswordSRP Password { get; set; }
        public int Period { get; set; }
        public Account.TLTmpPassword Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Password = (TLAbsInputCheckPasswordSRP)ObjectUtils.DeserializeObject(br);
            Period = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Password, bw);
            bw.Write(Period);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Account.TLTmpPassword)ObjectUtils.DeserializeObject(br);

        }
    }
}
