using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-1212732749)]
    public class TLPasswordSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1212732749;
            }
        }

        public string Email { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Email = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Email, bw);
        }
    }
}
