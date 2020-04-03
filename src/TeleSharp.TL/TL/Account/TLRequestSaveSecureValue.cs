using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1986010339)]
    public class TLRequestSaveSecureValue : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1986010339;
            }
        }

        public TLInputSecureValue Value { get; set; }
        public long SecureSecretId { get; set; }
        public TLSecureValue Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Value = (TLInputSecureValue)ObjectUtils.DeserializeObject(br);
            SecureSecretId = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Value, bw);
            bw.Write(SecureSecretId);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLSecureValue)ObjectUtils.DeserializeObject(br);

        }
    }
}
