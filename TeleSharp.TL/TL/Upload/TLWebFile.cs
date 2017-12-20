using System.IO;

namespace TeleSharp.TL.Upload
{
    [TLObject(568808380)]
    public class TLWebFile : TLObject
    {
        public byte[] Bytes { get; set; }

        public override int Constructor
        {
            get
            {
                return 568808380;
            }
        }

        public Storage.TLAbsFileType FileType { get; set; }

        public string MimeType { get; set; }

        public int Mtime { get; set; }

        public int Size { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Size = br.ReadInt32();
            MimeType = StringUtil.Deserialize(br);
            FileType = (Storage.TLAbsFileType)ObjectUtils.DeserializeObject(br);
            Mtime = br.ReadInt32();
            Bytes = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Size);
            StringUtil.Serialize(MimeType, bw);
            ObjectUtils.SerializeObject(FileType, bw);
            bw.Write(Mtime);
            BytesUtil.Serialize(Bytes, bw);
        }
    }
}
