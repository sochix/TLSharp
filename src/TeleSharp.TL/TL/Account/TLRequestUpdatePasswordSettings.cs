using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1516564433)]
    public class TLRequestUpdatePasswordSettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1516564433;
            }
        }

        public TLAbsInputCheckPasswordSRP Password { get; set; }
        public Account.TLPasswordInputSettings NewSettings { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Password = (TLAbsInputCheckPasswordSRP)ObjectUtils.DeserializeObject(br);
            NewSettings = (Account.TLPasswordInputSettings)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Password, bw);
            ObjectUtils.SerializeObject(NewSettings, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
