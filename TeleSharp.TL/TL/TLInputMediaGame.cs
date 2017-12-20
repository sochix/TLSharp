using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-750828557)]
    public class TLInputMediaGame : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -750828557;
            }
        }

        public TLAbsInputGame Id { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLAbsInputGame)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
        }
    }
}
