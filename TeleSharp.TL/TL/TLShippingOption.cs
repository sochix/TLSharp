using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1239335713)]
    public class TLShippingOption : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1239335713;
            }
        }

        public string id { get; set; }
        public string title { get; set; }
        public TLVector<TLLabeledPrice> prices { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = StringUtil.Deserialize(br);
            title = StringUtil.Deserialize(br);
            prices = (TLVector<TLLabeledPrice>)ObjectUtils.DeserializeVector<TLLabeledPrice>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(id, bw);
            StringUtil.Serialize(title, bw);
            ObjectUtils.SerializeObject(prices, bw);

        }
    }
}
