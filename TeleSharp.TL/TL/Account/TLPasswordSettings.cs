using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-1212732749)]
    public class TLPasswordSettings : TLObject
    {
        public override int Constructor => -1212732749;

        public string email { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            email = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(email, bw);
        }
    }
}