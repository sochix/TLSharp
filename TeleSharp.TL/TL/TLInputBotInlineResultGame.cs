using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1336154098)]
    public class TLInputBotInlineResultGame : TLAbsInputBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return 1336154098;
            }
        }

        public string id { get; set; }
        public string short_name { get; set; }
        public TLAbsInputBotInlineMessage send_message { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = StringUtil.Deserialize(br);
            short_name = StringUtil.Deserialize(br);
            send_message = (TLAbsInputBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(id, bw);
            StringUtil.Serialize(short_name, bw);
            ObjectUtils.SerializeObject(send_message, bw);

        }
    }
}
