using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1109588596)]
    public class TLRequestGetMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1109588596;
            }
        }

        public TLVector<int> Id { get; set; }

        public Messages.TLAbsMessages Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
        }
    }
}
