using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-162681021)]
    public class TLRequestRequestEncryption : TLMethod
    {
        public override int Constructor => -162681021;

        public TLAbsInputUser user_id { get; set; }
        public int random_id { get; set; }
        public byte[] g_a { get; set; }
        public TLAbsEncryptedChat Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = (TLAbsInputUser) ObjectUtils.DeserializeObject(br);
            random_id = br.ReadInt32();
            g_a = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(user_id, bw);
            bw.Write(random_id);
            BytesUtil.Serialize(g_a, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsEncryptedChat) ObjectUtils.DeserializeObject(br);
        }
    }
}