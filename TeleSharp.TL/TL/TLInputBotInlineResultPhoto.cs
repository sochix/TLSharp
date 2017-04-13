using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1462213465)]
    public class TLInputBotInlineResultPhoto : TLAbsInputBotInlineResult
    {
        public override int Constructor => -1462213465;

        public string id { get; set; }
        public string type { get; set; }
        public TLAbsInputPhoto photo { get; set; }
        public TLAbsInputBotInlineMessage send_message { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = StringUtil.Deserialize(br);
            type = StringUtil.Deserialize(br);
            photo = (TLAbsInputPhoto) ObjectUtils.DeserializeObject(br);
            send_message = (TLAbsInputBotInlineMessage) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(id, bw);
            StringUtil.Serialize(type, bw);
            ObjectUtils.SerializeObject(photo, bw);
            ObjectUtils.SerializeObject(send_message, bw);
        }
    }
}