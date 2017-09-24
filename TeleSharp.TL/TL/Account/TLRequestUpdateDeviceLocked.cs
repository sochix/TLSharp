using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(954152242)]
    public class TLRequestUpdateDeviceLocked : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 954152242;
            }
        }

        public int period { get; set; }
        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            period = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(period);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}