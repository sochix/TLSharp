using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-750828557)]
    public class TLInputMediaGame : TLAbsInputMedia
    {
        public override int Constructor => -750828557;

        public TLAbsInputGame id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputGame) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
        }
    }
}