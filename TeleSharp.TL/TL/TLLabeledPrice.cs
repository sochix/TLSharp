using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-886477832)]
    public class TLLabeledPrice : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -886477832;
            }
        }

        public string label { get; set; }
        public long amount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            label = StringUtil.Deserialize(br);
            amount = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(label, bw);
            bw.Write(amount);

        }
    }
}
