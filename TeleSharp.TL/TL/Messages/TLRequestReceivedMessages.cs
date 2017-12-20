using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(94983360)]
    public class TLRequestReceivedMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 94983360;
            }
        }

        public int MaxId { get; set; }

        public TLVector<TLReceivedNotifyMessage> Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            MaxId = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLReceivedNotifyMessage>)ObjectUtils.DeserializeVector<TLReceivedNotifyMessage>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(MaxId);
        }
    }
}
