using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(1418342645)]
    public class TLRequestGetPassword : TLMethod
    {
        public override int Constructor => 1418342645;

        public TLAbsPassword Response { get; set; }


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
            Response = (TLAbsPassword) ObjectUtils.DeserializeObject(br);
        }
    }
}