using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1369162417)]
    public class TLRequestUploadMedia : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1369162417;
            }
        }

        public TLAbsInputMedia Media { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public TLAbsMessageMedia Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Media = (TLAbsInputMedia)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            ObjectUtils.SerializeObject(Media, bw);
        }
    }
}
