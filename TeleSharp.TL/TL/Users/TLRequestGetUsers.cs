using System.IO;

namespace TeleSharp.TL.Users
{
    [TLObject(227648840)]
    public class TLRequestGetUsers : TLMethod
    {
        public override int Constructor => 227648840;

        public TLVector<TLAbsInputUser> id { get; set; }
        public TLVector<TLAbsUser> Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = ObjectUtils.DeserializeVector<TLAbsInputUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }
    }
}