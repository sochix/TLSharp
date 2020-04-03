using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1823064809)]
    public class TLPollAnswer : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1823064809;
            }
        }

        public string Text { get; set; }
        public byte[] Option { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = StringUtil.Deserialize(br);
            Option = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Text, bw);
            BytesUtil.Serialize(Option, bw);

        }
    }
}
