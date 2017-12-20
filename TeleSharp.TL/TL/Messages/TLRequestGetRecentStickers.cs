using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1587647177)]
    public class TLRequestGetRecentStickers : TLMethod
    {
        public bool Attached { get; set; }

        public override int Constructor
        {
            get
            {
                return 1587647177;
            }
        }

        public int Flags { get; set; }

        public int Hash { get; set; }

        public Messages.TLAbsRecentStickers Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Attached = (Flags & 1) != 0;
            Hash = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsRecentStickers)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(Hash);
        }
    }
}
