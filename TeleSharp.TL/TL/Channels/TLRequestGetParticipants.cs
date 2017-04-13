using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(618237842)]
    public class TLRequestGetParticipants : TLMethod
    {
        public override int Constructor => 618237842;

        public TLAbsInputChannel channel { get; set; }
        public TLAbsChannelParticipantsFilter filter { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public TLChannelParticipants Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel) ObjectUtils.DeserializeObject(br);
            filter = (TLAbsChannelParticipantsFilter) ObjectUtils.DeserializeObject(br);
            offset = br.ReadInt32();
            limit = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(filter, bw);
            bw.Write(offset);
            bw.Write(limit);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLChannelParticipants) ObjectUtils.DeserializeObject(br);
        }
    }
}