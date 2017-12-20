using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-878758099)]
    public class TLRequestInvokeAfterMsg : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -878758099;
            }
        }

        public long MsgId { get; set; }

        public TLObject Query { get; set; }

        public TLObject Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            MsgId = br.ReadInt64();
            Query = (TLObject)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLObject)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(MsgId);
            ObjectUtils.SerializeObject(Query, bw);
        }
    }
}
