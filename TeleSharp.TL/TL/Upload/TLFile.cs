using System.IO;
using TeleSharp.TL.Storage;

namespace TeleSharp.TL.Upload
{
    [TLObject(157948117)]
    public class TLFile : TLObject
    {
        public override int Constructor => 157948117;

        public TLAbsFileType type { get; set; }
        public int mtime { get; set; }
        public byte[] bytes { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            type = (TLAbsFileType) ObjectUtils.DeserializeObject(br);
            mtime = br.ReadInt32();
            bytes = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(type, bw);
            bw.Write(mtime);
            BytesUtil.Serialize(bytes, bw);
        }
    }
}