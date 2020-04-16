using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
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

        public string Id { get; set; }
        public string Title { get; set; }
        public TLVector<TLLabeledPrice> Prices { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = StringUtil.Deserialize(br);
            Title = StringUtil.Deserialize(br);
            Prices = (TLVector<TLLabeledPrice>)ObjectUtils.DeserializeVector<TLLabeledPrice>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Id, bw);
            StringUtil.Serialize(Title, bw);
            ObjectUtils.SerializeObject(Prices, bw);
        }
    }
}
