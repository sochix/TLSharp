using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(890549214)]
    public class TLRequestUpdateUsername : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 890549214;
            }
        }

        public TLAbsInputChannel channel { get; set; }
        public string username { get; set; }
        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            username = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            StringUtil.Serialize(username, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}