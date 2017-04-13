using System.IO;

namespace TeleSharp.TL.Updates
{
    [TLObject(-1154295872)]
    public class TLRequestGetChannelDifference : TLMethod
    {
        public override int Constructor => -1154295872;

        public TLAbsInputChannel channel { get; set; }
        public TLAbsChannelMessagesFilter filter { get; set; }
        public int pts { get; set; }
        public int limit { get; set; }
        public TLAbsChannelDifference Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel) ObjectUtils.DeserializeObject(br);
            filter = (TLAbsChannelMessagesFilter) ObjectUtils.DeserializeObject(br);
            pts = br.ReadInt32();
            limit = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(filter, bw);
            bw.Write(pts);
            bw.Write(limit);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsChannelDifference) ObjectUtils.DeserializeObject(br);
        }
    }
}