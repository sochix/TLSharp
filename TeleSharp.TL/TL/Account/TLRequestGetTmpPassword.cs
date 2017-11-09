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

        public byte[] password_hash { get; set; }
        public int period { get; set; }
        public Account.TLTmpPassword Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            password_hash = BytesUtil.Deserialize(br);
            period = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(password_hash, bw);
            bw.Write(period);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Account.TLTmpPassword)ObjectUtils.DeserializeObject(br);

        }
    }
}
