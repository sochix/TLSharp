using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1436924774)]
    public class TLRequestReceivedQueue : TLMethod
    {
        public override int Constructor => 1436924774;

        public int max_qts { get; set; }
        public TLVector<long> Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            max_qts = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(max_qts);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = ObjectUtils.DeserializeVector<long>(br);
        }
    }
}