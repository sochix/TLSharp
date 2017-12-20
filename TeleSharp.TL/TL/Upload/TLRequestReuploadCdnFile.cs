using System.IO;

namespace TeleSharp.TL.Upload
{
    [TLObject(452533257)]
    public class TLRequestReuploadCdnFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 452533257;
            }
        }

        public byte[] FileToken { get; set; }

        public byte[] RequestToken { get; set; }

        public TLVector<TLCdnFileHash> Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            FileToken = BytesUtil.Deserialize(br);
            RequestToken = BytesUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLCdnFileHash>)ObjectUtils.DeserializeVector<TLCdnFileHash>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(FileToken, bw);
            BytesUtil.Serialize(RequestToken, bw);
        }
    }
}
