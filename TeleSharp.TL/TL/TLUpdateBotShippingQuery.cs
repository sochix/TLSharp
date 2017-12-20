using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-523384512)]
    public class TLUpdateBotShippingQuery : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -523384512;
            }
        }

        public byte[] Payload { get; set; }

        public long QueryId { get; set; }

        public TLPostAddress ShippingAddress { get; set; }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            QueryId = br.ReadInt64();
            UserId = br.ReadInt32();
            Payload = BytesUtil.Deserialize(br);
            ShippingAddress = (TLPostAddress)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(QueryId);
            bw.Write(UserId);
            BytesUtil.Serialize(Payload, bw);
            ObjectUtils.SerializeObject(ShippingAddress, bw);
        }
    }
}
