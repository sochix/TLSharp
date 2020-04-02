using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1462213465)]
    public class TLInputBotInlineResultPhoto : TLAbsInputBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return -1462213465;
            }
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public TLAbsInputPhoto Photo { get; set; }
        public TLAbsInputBotInlineMessage SendMessage { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = StringUtil.Deserialize(br);
            Type = StringUtil.Deserialize(br);
            Photo = (TLAbsInputPhoto)ObjectUtils.DeserializeObject(br);
            SendMessage = (TLAbsInputBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Id, bw);
            StringUtil.Serialize(Type, bw);
            ObjectUtils.SerializeObject(Photo, bw);
            ObjectUtils.SerializeObject(SendMessage, bw);

        }
    }
}
