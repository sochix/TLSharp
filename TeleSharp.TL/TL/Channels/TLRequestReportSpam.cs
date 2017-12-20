using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-32999408)]
    public class TLRequestReportSpam : TLMethod
    {
        public TLAbsInputChannel Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return -32999408;
            }
        }

        public TLVector<int> Id { get; set; }

        public bool Response { get; set; }

        public TLAbsInputUser UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
            ObjectUtils.SerializeObject(UserId, bw);
            ObjectUtils.SerializeObject(Id, bw);
        }
    }
}
