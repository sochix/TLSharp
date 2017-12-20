using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(2031374829)]
    public class TLRequestSetEncryptedTyping : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2031374829;
            }
        }

        public TLInputEncryptedChat Peer { get; set; }

        public bool Response { get; set; }

        public bool Typing { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
            Typing = BoolUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            BoolUtil.Serialize(Typing, bw);
        }
    }
}
