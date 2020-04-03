using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1410748418)]
    public class TLMessageActionBotAllowed : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -1410748418;
            }
        }

        public string Domain { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Domain = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Domain, bw);

        }
    }
}
