using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(865483769)]
    public class TLRequestForwardMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 865483769;
            }
        }

        public int Id { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public long RandomId { get; set; }

        public TLAbsUpdates Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Id = br.ReadInt32();
            RandomId = br.ReadInt64();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(Id);
            bw.Write(RandomId);
        }
    }
}
