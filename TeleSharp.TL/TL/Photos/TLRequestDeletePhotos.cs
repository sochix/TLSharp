using System.IO;

namespace TeleSharp.TL.Photos
{
    [TLObject(-2016444625)]
    public class TLRequestDeletePhotos : TLMethod
    {
        public override int Constructor => -2016444625;

        public TLVector<TLAbsInputPhoto> id { get; set; }
        public TLVector<long> Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = ObjectUtils.DeserializeVector<TLAbsInputPhoto>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = ObjectUtils.DeserializeVector<long>(br);
        }
    }
}