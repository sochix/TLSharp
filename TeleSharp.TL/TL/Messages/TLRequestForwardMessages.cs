using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1888354709)]
    public class TLRequestForwardMessages : TLMethod
    {
        public bool Background { get; set; }

        public override int Constructor
        {
            get
            {
                return 1888354709;
            }
        }

        public int Flags { get; set; }

        public TLAbsInputPeer FromPeer { get; set; }

        public TLVector<int> Id { get; set; }

        public TLVector<long> RandomId { get; set; }

        public TLAbsUpdates Response { get; set; }

        public bool Silent { get; set; }

        public TLAbsInputPeer ToPeer { get; set; }

        public bool WithMyScore { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Silent = (Flags & 32) != 0;
            Background = (Flags & 64) != 0;
            WithMyScore = (Flags & 256) != 0;
            FromPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
            RandomId = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            ToPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



            ObjectUtils.SerializeObject(FromPeer, bw);
            ObjectUtils.SerializeObject(Id, bw);
            ObjectUtils.SerializeObject(RandomId, bw);
            ObjectUtils.SerializeObject(ToPeer, bw);
        }
    }
}
