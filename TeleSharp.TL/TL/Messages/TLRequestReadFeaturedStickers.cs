using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1527873830)]
    public class TLRequestReadFeaturedStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1527873830;
            }
        }

        public TLVector<long> Id { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
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
