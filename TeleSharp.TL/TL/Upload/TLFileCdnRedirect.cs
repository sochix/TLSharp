using System.IO;
namespace TeleSharp.TL.Upload
{
    [TLObject(-363659686)]
    public class TLFileCdnRedirect : TLAbsFile
    {
        public override int Constructor
        {
            get
            {
                return -363659686;
            }
        }

        public int dc_id { get; set; }
        public byte[] file_token { get; set; }
        public byte[] encryption_key { get; set; }
        public byte[] encryption_iv { get; set; }
        public TLVector<TLCdnFileHash> cdn_file_hashes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            dc_id = br.ReadInt32();
            file_token = BytesUtil.Deserialize(br);
            encryption_key = BytesUtil.Deserialize(br);
            encryption_iv = BytesUtil.Deserialize(br);
            cdn_file_hashes = (TLVector<TLCdnFileHash>)ObjectUtils.DeserializeVector<TLCdnFileHash>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(dc_id);
            BytesUtil.Serialize(file_token, bw);
            BytesUtil.Serialize(encryption_key, bw);
            BytesUtil.Serialize(encryption_iv, bw);
            ObjectUtils.SerializeObject(cdn_file_hashes, bw);

        }
    }
}
