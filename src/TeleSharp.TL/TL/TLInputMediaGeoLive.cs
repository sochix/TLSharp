using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-833715459)]
    public class TLInputMediaGeoLive : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -833715459;
            }
        }

        public int Flags { get; set; }
        public bool Stopped { get; set; }
        public TLAbsInputGeoPoint GeoPoint { get; set; }
        public int? Period { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Stopped = (Flags & 1) != 0;
            GeoPoint = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);
            if ((Flags & 2) != 0)
                Period = br.ReadInt32();
            else
                Period = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(GeoPoint, bw);
            if ((Flags & 2) != 0)
                bw.Write(Period.Value);

        }
    }
}
