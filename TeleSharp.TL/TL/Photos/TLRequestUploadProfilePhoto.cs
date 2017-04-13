using System.IO;

namespace TeleSharp.TL.Photos
{
    [TLObject(1328726168)]
    public class TLRequestUploadProfilePhoto : TLMethod
    {
        public override int Constructor => 1328726168;

        public TLAbsInputFile file { get; set; }
        public TLPhoto Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            file = (TLAbsInputFile) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(file, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLPhoto) ObjectUtils.DeserializeObject(br);
        }
    }
}