using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(455635795)]
    public class TLMessageActionSecureValuesSentMe : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return 455635795;
            }
        }

        public TLVector<TLSecureValue> Values { get; set; }
        public TLSecureCredentialsEncrypted Credentials { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Values = (TLVector<TLSecureValue>)ObjectUtils.DeserializeVector<TLSecureValue>(br);
            Credentials = (TLSecureCredentialsEncrypted)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Values, bw);
            ObjectUtils.SerializeObject(Credentials, bw);
        }
    }
}
