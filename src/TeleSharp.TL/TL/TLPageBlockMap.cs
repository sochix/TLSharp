using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1538310410)]
    public class TLPageBlockMap : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1538310410;
            }
        }

        public TLAbsGeoPoint Geo { get; set; }
        public int Zoom { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public TLPageCaption Caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            Zoom = br.ReadInt32();
            W = br.ReadInt32();
            H = br.ReadInt32();
            Caption = (TLPageCaption)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Geo, bw);
            bw.Write(Zoom);
            bw.Write(W);
            bw.Write(H);
            ObjectUtils.SerializeObject(Caption, bw);

        }
    }
}
