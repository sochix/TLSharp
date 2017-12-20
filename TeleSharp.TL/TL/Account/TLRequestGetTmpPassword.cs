using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(1250046590)]
    public class TLRequestGetTmpPassword : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1250046590;
            }
        }

        public byte[] PasswordHash { get; set; }

        public int Period { get; set; }

        public Account.TLTmpPassword Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PasswordHash = BytesUtil.Deserialize(br);
            Period = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Account.TLTmpPassword)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(PasswordHash, bw);
            bw.Write(Period);
        }
    }
}
