using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-104578748)]
    public class TLInputMediaGeoPoint : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -104578748;
            }
        }

        public TLAbsInputGeoPoint geo_point { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            geo_point = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(geo_point, bw);

        }
    }
}
