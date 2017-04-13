using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1361650766)]
    public class TLMaskCoords : TLObject
    {
        public override int Constructor => -1361650766;

        public int n { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double zoom { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            n = br.ReadInt32();
            x = br.ReadDouble();
            y = br.ReadDouble();
            zoom = br.ReadDouble();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(n);
            bw.Write(x);
            bw.Write(y);
            bw.Write(zoom);
        }
    }
}