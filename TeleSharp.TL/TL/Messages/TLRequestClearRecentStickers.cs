using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1986437075)]
    public class TLRequestClearRecentStickers : TLMethod
    {
        public bool Attached { get; set; }

        public override int Constructor
        {
            get
            {
                return -1986437075;
            }
        }

        public int Flags { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Attached = (Flags & 1) != 0;
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
        }
    }
}
