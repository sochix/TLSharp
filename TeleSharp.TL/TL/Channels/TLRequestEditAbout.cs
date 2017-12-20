using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(333610782)]
    public class TLRequestEditAbout : TLMethod
    {
        public string About { get; set; }

        public TLAbsInputChannel Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return 333610782;
            }
        }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            About = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
            StringUtil.Serialize(About, bw);
        }
    }
}
