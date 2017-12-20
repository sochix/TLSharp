using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(1099779595)]
    public class TLRequestDeleteAccount : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1099779595;
            }
        }

        public string Reason { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Reason = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Reason, bw);
        }
    }
}
