using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(766298703)]
    public class TLRequestGetFeaturedStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 766298703;
            }
        }

        public int Hash { get; set; }

        public Messages.TLAbsFeaturedStickers Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsFeaturedStickers)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);
        }
    }
}
