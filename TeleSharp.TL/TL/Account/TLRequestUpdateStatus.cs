using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(1713919532)]
    public class TLRequestUpdateStatus : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1713919532;
            }
        }

        public bool Offline { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Offline = BoolUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BoolUtil.Serialize(Offline, bw);
        }
    }
}
