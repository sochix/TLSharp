using System.IO;
namespace TeleSharp.TL.Upload
{
    [TLObject(-149567365)]
    public class TLRequestGetCdnFileHashes : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -149567365;
            }
        }

        public byte[] file_token { get; set; }
        public int offset { get; set; }
        public TLVector<TLCdnFileHash> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            file_token = BytesUtil.Deserialize(br);
            offset = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(file_token, bw);
            bw.Write(offset);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLCdnFileHash>)ObjectUtils.DeserializeVector<TLCdnFileHash>(br);

        }
    }
}
