using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-350980120)]
    public class TLWebPageEmpty : TLAbsWebPage
    {
        public override int Constructor
        {
            get
            {
                return -350980120;
            }
        }

        public long id { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
        }
    }
}