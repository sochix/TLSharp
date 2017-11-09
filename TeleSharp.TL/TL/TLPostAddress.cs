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

        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryIso2 { get; set; }
        public string PostCode { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            StreetLine1 = StringUtil.Deserialize(br);
            StreetLine2 = StringUtil.Deserialize(br);
            City = StringUtil.Deserialize(br);
            State = StringUtil.Deserialize(br);
            CountryIso2 = StringUtil.Deserialize(br);
            PostCode = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(StreetLine1, bw);
            StringUtil.Serialize(StreetLine2, bw);
            StringUtil.Serialize(City, bw);
            StringUtil.Serialize(State, bw);
            StringUtil.Serialize(CountryIso2, bw);
            StringUtil.Serialize(PostCode, bw);

        }
    }
}
