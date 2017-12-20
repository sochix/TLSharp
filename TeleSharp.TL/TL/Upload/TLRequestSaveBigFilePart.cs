using System.IO;

namespace TeleSharp.TL.Upload
{
    [TLObject(-562337987)]
    public class TLRequestSaveBigFilePart : TLMethod
    {
        public byte[] Bytes { get; set; }

        public override int Constructor
        {
            get
            {
                return -562337987;
            }
        }

        public long FileId { get; set; }

        public int FilePart { get; set; }

        public int FileTotalParts { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            FileId = br.ReadInt64();
            FilePart = br.ReadInt32();
            FileTotalParts = br.ReadInt32();
            Bytes = BytesUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(FileId);
            bw.Write(FilePart);
            bw.Write(FileTotalParts);
            BytesUtil.Serialize(Bytes, bw);
        }
    }
}
