using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(864953444)]
    public class TLRequestGetDocumentByHash : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 864953444;
            }
        }

        public string MimeType { get; set; }

        public TLAbsDocument Response { get; set; }

        public byte[] Sha256 { get; set; }

        public int Size { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Sha256 = BytesUtil.Deserialize(br);
            Size = br.ReadInt32();
            MimeType = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(Sha256, bw);
            bw.Write(Size);
            StringUtil.Serialize(MimeType, bw);
        }
    }
}
