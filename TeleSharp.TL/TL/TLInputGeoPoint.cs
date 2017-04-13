using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-206066487)]
    public class TLInputGeoPoint : TLAbsInputGeoPoint
    {
        public override int Constructor => -206066487;

        public double lat { get; set; }
        public double @long { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            lat = br.ReadDouble();
            @long = br.ReadDouble();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(lat);
            bw.Write(@long);
        }
    }
}