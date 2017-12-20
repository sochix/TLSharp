using System.IO;

namespace TeleSharp.TL.Users
{
    [TLObject(227648840)]
    public class TLRequestGetUsers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 227648840;
            }
        }

        public TLVector<TLAbsInputUser> Id { get; set; }

        public TLVector<TLAbsUser> Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLVector<TLAbsInputUser>)ObjectUtils.DeserializeVector<TLAbsInputUser>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
        }
    }
}
