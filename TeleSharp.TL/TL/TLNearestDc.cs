using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1910892683)]
    public class TLNearestDc : TLObject
    {
        public override int Constructor => -1910892683;

        public string country { get; set; }
        public int this_dc { get; set; }
        public int nearest_dc { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            country = StringUtil.Deserialize(br);
            this_dc = br.ReadInt32();
            nearest_dc = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(country, bw);
            bw.Write(this_dc);
            bw.Write(nearest_dc);
        }
    }
}