using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(618237842)]
    public class TLRequestGetParticipants : TLMethod
    {
        public TLAbsInputChannel Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return 618237842;
            }
        }

        public TLAbsChannelParticipantsFilter Filter { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public Channels.TLChannelParticipants Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Filter = (TLAbsChannelParticipantsFilter)ObjectUtils.DeserializeObject(br);
            Offset = br.ReadInt32();
            Limit = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Channels.TLChannelParticipants)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
            ObjectUtils.SerializeObject(Filter, bw);
            bw.Write(Offset);
            bw.Write(Limit);
        }
    }
}
