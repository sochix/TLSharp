using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1251549527)]
    public class TLInputStickeredMediaPhoto : TLAbsInputStickeredMedia
    {
        public override int Constructor => 1251549527;

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