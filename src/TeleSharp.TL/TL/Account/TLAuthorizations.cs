using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(307276766)]
    public class TLAuthorizations : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 307276766;
            }
        }

        public TLVector<TLAuthorization> Authorizations { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Authorizations = (TLVector<TLAuthorization>)ObjectUtils.DeserializeVector<TLAuthorization>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Authorizations, bw);

        }
    }
}
