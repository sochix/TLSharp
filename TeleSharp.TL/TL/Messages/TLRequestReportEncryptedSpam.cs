using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1259113487)]
    public class TLRequestReportEncryptedSpam : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1259113487;
            }
        }

        public TLInputEncryptedChat peer { get; set; }
        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}