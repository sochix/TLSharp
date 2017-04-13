using System.IO;
using TeleSharp.TL.Messages;

namespace TeleSharp.TL.Channels
{
    [TLObject(-1814580409)]
    public class TLRequestGetMessages : TLMethod
    {
        public override int Constructor => -1814580409;

        public TLAbsInputChannel channel { get; set; }
        public TLVector<int> id { get; set; }
        public TLAbsMessages Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel) ObjectUtils.DeserializeObject(br);
            id = ObjectUtils.DeserializeVector<int>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsMessages) ObjectUtils.DeserializeObject(br);
        }
    }
}