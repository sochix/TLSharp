using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(-1189013126)]
    public class TLRequestGetAppChangelog : TLMethod
    {
        public override int Constructor => -1189013126;

        public TLAbsAppChangelog Response { get; set; }


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
            Response = (TLAbsAppChangelog) ObjectUtils.DeserializeObject(br);
        }
    }
}