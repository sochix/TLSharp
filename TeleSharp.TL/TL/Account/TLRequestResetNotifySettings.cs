using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-612493497)]
    public class TLRequestResetNotifySettings : TLMethod
    {
        public override int Constructor => -612493497;

        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}