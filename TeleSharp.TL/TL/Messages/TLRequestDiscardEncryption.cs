using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-304536635)]
    public class TLRequestDiscardEncryption : TLMethod
    {
        public override int Constructor => -304536635;

        public int chat_id { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}