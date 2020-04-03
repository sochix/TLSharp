using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(-242427324)]
    public class TLFileCdnRedirect : TLAbsFile
    {
        public override int Constructor
        {
            get
            {
                return -242427324;
            }
        }

        public int DcId { get; set; }
        public byte[] FileToken { get; set; }
        public byte[] EncryptionKey { get; set; }
        public byte[] EncryptionIv { get; set; }
        public TLVector<TLFileHash> FileHashes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            DcId = br.ReadInt32();
            FileToken = BytesUtil.Deserialize(br);
            EncryptionKey = BytesUtil.Deserialize(br);
            EncryptionIv = BytesUtil.Deserialize(br);
            FileHashes = (TLVector<TLFileHash>)ObjectUtils.DeserializeVector<TLFileHash>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(DcId);
            BytesUtil.Serialize(FileToken, bw);
            BytesUtil.Serialize(EncryptionKey, bw);
            BytesUtil.Serialize(EncryptionIv, bw);
            ObjectUtils.SerializeObject(FileHashes, bw);

        }
    }
}
