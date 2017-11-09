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

        public string Id { get; set; }
        public string ShortName { get; set; }
        public TLAbsInputBotInlineMessage SendMessage { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = StringUtil.Deserialize(br);
            ShortName = StringUtil.Deserialize(br);
            SendMessage = (TLAbsInputBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Id, bw);
            StringUtil.Serialize(ShortName, bw);
            ObjectUtils.SerializeObject(SendMessage, bw);

        }
    }
}
