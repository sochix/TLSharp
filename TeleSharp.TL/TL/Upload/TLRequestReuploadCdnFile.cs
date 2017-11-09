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

        public byte[] file_token { get; set; }
        public byte[] request_token { get; set; }
        public TLVector<TLCdnFileHash> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            file_token = BytesUtil.Deserialize(br);
            request_token = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(file_token, bw);
            BytesUtil.Serialize(request_token, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLCdnFileHash>)ObjectUtils.DeserializeVector<TLCdnFileHash>(br);

        }
    }
}
