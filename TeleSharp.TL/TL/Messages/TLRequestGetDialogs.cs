using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(421243333)]
    public class TLRequestGetDialogs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 421243333;
            }
        }

        public bool ExcludePinned { get; set; }

        public int Flags { get; set; }

        public int Limit { get; set; }

        public int OffsetDate { get; set; }

        public int OffsetId { get; set; }

        public TLAbsInputPeer OffsetPeer { get; set; }

        public Messages.TLAbsDialogs Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ExcludePinned = (Flags & 1) != 0;
            OffsetDate = br.ReadInt32();
            OffsetId = br.ReadInt32();
            OffsetPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Limit = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsDialogs)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(OffsetDate);
            bw.Write(OffsetId);
            ObjectUtils.SerializeObject(OffsetPeer, bw);
            bw.Write(Limit);
        }
    }
}
