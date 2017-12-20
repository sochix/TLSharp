using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(520357240)]
    public class TLRequestCancelCode : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 520357240;
            }
        }

        public string PhoneCodeHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhoneNumber = StringUtil.Deserialize(br);
            PhoneCodeHash = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(PhoneNumber, bw);
            StringUtil.Serialize(PhoneCodeHash, bw);
        }
    }
}
