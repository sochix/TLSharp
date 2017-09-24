using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-38694904)]
    public class TLMessageMediaGame : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return -38694904;
            }
        }

        public TLGame game { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            game = (TLGame)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(game, bw);
        }
    }
}