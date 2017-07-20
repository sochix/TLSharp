using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1008755359)]
    public class TLInlineBotSwitchPM : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1008755359;
            }
        }

        public string text { get; set; }
        public string start_param { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = StringUtil.Deserialize(br);
            start_param = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(text, bw);
            StringUtil.Serialize(start_param, bw);

        }
    }
}
