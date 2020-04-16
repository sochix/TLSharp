using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Account
{
    [TLObject(1936088002)]
    public class TLRequestGetSecureValue : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1936088002;
            }
        }

        public TLVector<TLAbsSecureValueType> Types { get; set; }
        public TLVector<TLSecureValue> Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Types = (TLVector<TLAbsSecureValueType>)ObjectUtils.DeserializeVector<TLAbsSecureValueType>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Types, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLSecureValue>)ObjectUtils.DeserializeVector<TLSecureValue>(br);
        }
    }
}
