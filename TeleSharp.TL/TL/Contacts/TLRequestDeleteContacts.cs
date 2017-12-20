using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(1504393374)]
    public class TLRequestDeleteContacts : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1504393374;
            }
        }

        public TLVector<TLAbsInputUser> Id { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLVector<TLAbsInputUser>)ObjectUtils.DeserializeVector<TLAbsInputUser>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
        }
    }
}
