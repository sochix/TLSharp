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

        public string reason { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            reason = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(reason, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
