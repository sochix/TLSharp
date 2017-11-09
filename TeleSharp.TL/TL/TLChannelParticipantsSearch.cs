using System.IO;
namespace TeleSharp.TL
{
    [TLObject(106343499)]
    public class TLChannelParticipantsSearch : TLAbsChannelParticipantsFilter
    {
        public override int Constructor
        {
            get
            {
                return 106343499;
            }
        }

        public string q { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            q = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(q, bw);

        }
    }
}
