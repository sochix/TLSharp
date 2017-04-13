using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(-1372724842)]
    public class TLRequestGetAppUpdate : TLMethod
    {
        public override int Constructor => -1372724842;

        public TLAbsAppUpdate Response { get; set; }


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
            Response = (TLAbsAppUpdate) ObjectUtils.DeserializeObject(br);
        }
    }
}