using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1837345356)]
    public class TLInputChatUploadedPhoto : TLAbsInputChatPhoto
    {
        public override int Constructor
        {
            get
            {
                return -1837345356;
            }
        }

        public TLAbsInputFile file { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            file = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(file, bw);
        }
    }
}