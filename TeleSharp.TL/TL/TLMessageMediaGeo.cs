using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1457575028)]
    public class TLMessageMediaGeo : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 1457575028;
            }
        }

        public TLAbsGeoPoint Geo { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Geo, bw);
        }
    }
}
