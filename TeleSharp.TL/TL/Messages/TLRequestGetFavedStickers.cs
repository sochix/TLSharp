using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(567151374)]
    public class TLRequestGetFavedStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 567151374;
            }
        }

        public int Hash { get; set; }

        public Messages.TLAbsFavedStickers Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsFavedStickers)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);
        }
    }
}
