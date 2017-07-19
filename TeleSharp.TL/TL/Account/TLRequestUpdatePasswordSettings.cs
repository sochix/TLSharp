using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-92517498)]
    public class TLRequestUpdatePasswordSettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -92517498;
            }
        }

        public byte[] current_password_hash { get; set; }
        public Account.TLPasswordInputSettings new_settings { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            current_password_hash = BytesUtil.Deserialize(br);
            new_settings = (Account.TLPasswordInputSettings)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(current_password_hash, bw);
            ObjectUtils.SerializeObject(new_settings, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
