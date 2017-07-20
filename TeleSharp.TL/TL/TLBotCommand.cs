using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1032140601)]
    public class TLBotCommand : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1032140601;
            }
        }

        public string command { get; set; }
        public string description { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            command = StringUtil.Deserialize(br);
            description = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(command, bw);
            StringUtil.Serialize(description, bw);

        }
    }
}
