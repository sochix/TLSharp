using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(479598769)]
    public class TLRequestGetAllStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 479598769;
            }
        }

        public int Hash { get; set; }

        public Messages.TLAbsAllStickers Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsAllStickers)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);
        }
    }
}
