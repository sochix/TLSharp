using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1503425638)]
    public class TLMessageActionChatCreate : TLAbsMessageAction
    {
        public override int Constructor => -1503425638;

        public string title { get; set; }
        public TLVector<int> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            title = StringUtil.Deserialize(br);
            users = ObjectUtils.DeserializeVector<int>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(title, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}