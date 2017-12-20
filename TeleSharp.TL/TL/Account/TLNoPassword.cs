using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-1764049896)]
    public class TLNoPassword : TLAbsPassword
    {
        public override int Constructor
        {
            get
            {
                return -1764049896;
            }
        }

        public string EmailUnconfirmedPattern { get; set; }

        public byte[] NewSalt { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            NewSalt = BytesUtil.Deserialize(br);
            EmailUnconfirmedPattern = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(NewSalt, bw);
            StringUtil.Serialize(EmailUnconfirmedPattern, bw);
        }
    }
}
