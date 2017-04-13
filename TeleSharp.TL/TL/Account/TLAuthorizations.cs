using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(307276766)]
    public class TLAuthorizations : TLObject
    {
        public override int Constructor => 307276766;

        public TLVector<TLAuthorization> authorizations { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            authorizations = ObjectUtils.DeserializeVector<TLAuthorization>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(authorizations, bw);
        }
    }
}