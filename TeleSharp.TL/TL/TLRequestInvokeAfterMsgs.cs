using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1036301552)]
    public class TLRequestInvokeAfterMsgs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1036301552;
            }
        }

        public TLVector<long> MsgIds { get; set; }

        public TLObject Query { get; set; }

        public TLObject Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            MsgIds = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            Query = (TLObject)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLObject)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(MsgIds, bw);
            ObjectUtils.SerializeObject(Query, bw);
        }
    }
}
