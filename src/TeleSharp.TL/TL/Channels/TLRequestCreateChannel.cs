using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(1029681423)]
    public class TLRequestCreateChannel : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1029681423;
            }
        }

        public int Flags { get; set; }
        public bool Broadcast { get; set; }
        public bool Megagroup { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public TLAbsInputGeoPoint GeoPoint { get; set; }
        public string Address { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Broadcast = (Flags & 1) != 0;
            Megagroup = (Flags & 2) != 0;
            Title = StringUtil.Deserialize(br);
            About = StringUtil.Deserialize(br);
            if ((Flags & 4) != 0)
                GeoPoint = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);
            else
                GeoPoint = null;

            if ((Flags & 4) != 0)
                Address = StringUtil.Deserialize(br);
            else
                Address = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(About, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(GeoPoint, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Address, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
