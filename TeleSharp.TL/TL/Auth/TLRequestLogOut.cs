using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(1461180992)]
    public class TLRequestLogOut : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1461180992;
            }
        }

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