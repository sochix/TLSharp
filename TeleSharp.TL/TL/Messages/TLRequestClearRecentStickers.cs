using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1986437075)]
    public class TLRequestClearRecentStickers : TLMethod
    {
        public override int Constructor => -1986437075;

        public int flags { get; set; }
        public bool attached { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = attached ? flags | 1 : flags & ~1;
        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            attached = (flags & 1) != 0;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}