using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-92517498)]
    public class TLRequestUpdatePasswordSettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -92517498;
            }
        }

        public byte[] CurrentPasswordHash { get; set; }

        public Account.TLPasswordInputSettings NewSettings { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            CurrentPasswordHash = BytesUtil.Deserialize(br);
            NewSettings = (Account.TLPasswordInputSettings)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(CurrentPasswordHash, bw);
            ObjectUtils.SerializeObject(NewSettings, bw);
        }
    }
}
