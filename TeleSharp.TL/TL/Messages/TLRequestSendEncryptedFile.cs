using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1701831834)]
    public class TLRequestSendEncryptedFile : TLMethod
    {
        public override int Constructor => -1701831834;

        public TLInputEncryptedChat peer { get; set; }
        public long random_id { get; set; }
        public byte[] data { get; set; }
        public TLAbsInputEncryptedFile file { get; set; }
        public TLAbsSentEncryptedMessage Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputEncryptedChat) ObjectUtils.DeserializeObject(br);
            random_id = br.ReadInt64();
            data = BytesUtil.Deserialize(br);
            file = (TLAbsInputEncryptedFile) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(random_id);
            BytesUtil.Serialize(data, bw);
            ObjectUtils.SerializeObject(file, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsSentEncryptedMessage) ObjectUtils.DeserializeObject(br);
        }
    }
}