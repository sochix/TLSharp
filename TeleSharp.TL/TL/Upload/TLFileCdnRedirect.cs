using System.IO;

namespace TeleSharp.TL.Upload
{
    [TLObject(-363659686)]
    public class TLFileCdnRedirect : TLAbsFile
    {
        public TLVector<TLCdnFileHash> CdnFileHashes { get; set; }

        public override int Constructor
        {
            get
            {
                return -363659686;
            }
        }

        public int DcId { get; set; }

        public byte[] EncryptionIv { get; set; }

        public byte[] EncryptionKey { get; set; }

        public byte[] FileToken { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            DcId = br.ReadInt32();
            FileToken = BytesUtil.Deserialize(br);
            EncryptionKey = BytesUtil.Deserialize(br);
            EncryptionIv = BytesUtil.Deserialize(br);
            CdnFileHashes = (TLVector<TLCdnFileHash>)ObjectUtils.DeserializeVector<TLCdnFileHash>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(DcId);
            BytesUtil.Serialize(FileToken, bw);
            BytesUtil.Serialize(EncryptionKey, bw);
            BytesUtil.Serialize(EncryptionIv, bw);
            ObjectUtils.SerializeObject(CdnFileHashes, bw);
        }
    }
}
