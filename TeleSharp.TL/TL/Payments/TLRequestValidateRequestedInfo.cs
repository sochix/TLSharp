using System.IO;

namespace TeleSharp.TL.Payments
{
    [TLObject(1997180532)]
    public class TLRequestValidateRequestedInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1997180532;
            }
        }

        public int Flags { get; set; }

        public TLPaymentRequestedInfo Info { get; set; }

        public int MsgId { get; set; }

        public Payments.TLValidatedRequestedInfo Response { get; set; }

        public bool Save { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Save = (Flags & 1) != 0;
            MsgId = br.ReadInt32();
            Info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Payments.TLValidatedRequestedInfo)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(MsgId);
            ObjectUtils.SerializeObject(Info, bw);
        }
    }
}
