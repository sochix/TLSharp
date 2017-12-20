using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(469850889)]
    public class TLRequestDeleteHistory : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 469850889;
            }
        }

        public int Flags { get; set; }

        public bool JustClear { get; set; }

        public int MaxId { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public Messages.TLAffectedHistory Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            JustClear = (Flags & 1) != 0;
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            MaxId = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAffectedHistory)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(MaxId);
        }
    }
}
