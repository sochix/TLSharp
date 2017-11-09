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

        public int hash { get; set; }
        public Messages.TLAbsFavedStickers Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(hash);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsFavedStickers)ObjectUtils.DeserializeObject(br);

        }
    }
}
