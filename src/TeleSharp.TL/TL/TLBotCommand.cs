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

        public string Command { get; set; }
        public string Description { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Command = StringUtil.Deserialize(br);
            Description = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Command, bw);
            StringUtil.Serialize(Description, bw);

        }
    }
}
