using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-471670279)]
    public class TLChannelParticipantCreator : TLAbsChannelParticipant
    {
        public override int Constructor => -471670279;

        public int user_id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
        }
    }
}