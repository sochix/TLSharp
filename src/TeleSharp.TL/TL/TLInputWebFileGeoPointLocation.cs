using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1625153079)]
    public class TLInputWebFileGeoPointLocation : TLAbsInputWebFileLocation
    {
        public override int Constructor
        {
            get
            {
                return -1625153079;
            }
        }

        public TLAbsInputGeoPoint GeoPoint { get; set; }
        public long AccessHash { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public int Zoom { get; set; }
        public int Scale { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            GeoPoint = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);
            AccessHash = br.ReadInt64();
            W = br.ReadInt32();
            H = br.ReadInt32();
            Zoom = br.ReadInt32();
            Scale = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(GeoPoint, bw);
            bw.Write(AccessHash);
            bw.Write(W);
            bw.Write(H);
            bw.Write(Zoom);
            bw.Write(Scale);

        }
    }
}
