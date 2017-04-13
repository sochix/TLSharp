using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(479598769)]
    public class TLRequestGetAllStickers : TLMethod
    {
        public override int Constructor => 479598769;

        public int hash { get; set; }
        public TLAbsAllStickers Response { get; set; }


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
            Response = (TLAbsAllStickers) ObjectUtils.DeserializeObject(br);
        }
    }
}