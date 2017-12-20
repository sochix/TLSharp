using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1844103547)]
    public class TLInputMediaInvoice : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -1844103547;
            }
        }

        public string Description { get; set; }

        public int Flags { get; set; }

        public TLInvoice Invoice { get; set; }

        public byte[] Payload { get; set; }

        public TLInputWebDocument Photo { get; set; }

        public string Provider { get; set; }

        public string StartParam { get; set; }

        public string Title { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Title = StringUtil.Deserialize(br);
            Description = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                Photo = (TLInputWebDocument)ObjectUtils.DeserializeObject(br);
            else
                Photo = null;

            Invoice = (TLInvoice)ObjectUtils.DeserializeObject(br);
            Payload = BytesUtil.Deserialize(br);
            Provider = StringUtil.Deserialize(br);
            StartParam = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(Description, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Photo, bw);
            ObjectUtils.SerializeObject(Invoice, bw);
            BytesUtil.Serialize(Payload, bw);
            StringUtil.Serialize(Provider, bw);
            StringUtil.Serialize(StartParam, bw);
        }
    }
}
