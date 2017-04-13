using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-326379039)]
    public class TLRequestToggleChatAdmins : TLMethod
    {
        public override int Constructor => -326379039;

        public int chat_id { get; set; }
        public bool enabled { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
            enabled = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
            BoolUtil.Serialize(enabled, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates) ObjectUtils.DeserializeObject(br);
        }
    }
}