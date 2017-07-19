using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(512535275)]
    public class TLPostAddress : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 512535275;
            }
        }

        public string street_line1 { get; set; }
        public string street_line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country_iso2 { get; set; }
        public string post_code { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            street_line1 = StringUtil.Deserialize(br);
            street_line2 = StringUtil.Deserialize(br);
            city = StringUtil.Deserialize(br);
            state = StringUtil.Deserialize(br);
            country_iso2 = StringUtil.Deserialize(br);
            post_code = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(street_line1, bw);
            StringUtil.Serialize(street_line2, bw);
            StringUtil.Serialize(city, bw);
            StringUtil.Serialize(state, bw);
            StringUtil.Serialize(country_iso2, bw);
            StringUtil.Serialize(post_code, bw);

        }
    }
}
