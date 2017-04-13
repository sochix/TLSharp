using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1991004873)]
    public class TLInputChatPhoto : TLAbsInputChatPhoto
    {
        public override int Constructor => -1991004873;

        public TLAbsInputPhoto id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputPhoto) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
        }
    }
}