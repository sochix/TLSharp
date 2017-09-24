using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1683826688)]
    public class TLChatEmpty : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return -1683826688;
            }
        }

        public int id { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
        }
    }
}