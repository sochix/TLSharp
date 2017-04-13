using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1527873830)]
    public class TLRequestReadFeaturedStickers : TLMethod
    {
        public override int Constructor => 1527873830;

        public TLVector<long> id { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = ObjectUtils.DeserializeVector<long>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}