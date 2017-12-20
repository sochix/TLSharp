using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(916930423)]
    public class TLRequestReadMessageContents : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 916930423;
            }
        }

        public TLVector<int> Id { get; set; }

        public Messages.TLAffectedMessages Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAffectedMessages)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
        }
    }
}
