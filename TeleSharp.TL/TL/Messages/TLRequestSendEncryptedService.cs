using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(852769188)]
    public class TLRequestSendEncryptedService : TLMethod
    {
        public override int Constructor => 852769188;

        public TLInputEncryptedChat peer { get; set; }
        public long random_id { get; set; }
        public byte[] data { get; set; }
        public TLAbsSentEncryptedMessage Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputEncryptedChat) ObjectUtils.DeserializeObject(br);
            random_id = br.ReadInt64();
            data = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(random_id);
            BytesUtil.Serialize(data, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsSentEncryptedMessage) ObjectUtils.DeserializeObject(br);
        }
    }
}