using System.IO;

namespace TeleSharp.TL.Upload
{
    [TLObject(157948117)]
    public class TLFile : TLAbsFile
    {
        public byte[] Bytes { get; set; }

        public override int Constructor
        {
            get
            {
                return 157948117;
            }
        }

        public int Mtime { get; set; }

        public Storage.TLAbsFileType Type { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Type = (Storage.TLAbsFileType)ObjectUtils.DeserializeObject(br);
            Mtime = br.ReadInt32();
            Bytes = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Type, bw);
            bw.Write(Mtime);
            BytesUtil.Serialize(Bytes, bw);
        }
    }
}
