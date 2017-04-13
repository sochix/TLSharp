using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-1068696894)]
    public class TLRequestGetWallPapers : TLMethod
    {
        public override int Constructor => -1068696894;

        public TLVector<TLAbsWallPaper> Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = ObjectUtils.DeserializeVector<TLAbsWallPaper>(br);
        }
    }
}