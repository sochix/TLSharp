using System.IO;
namespace TeleSharp.TL.Upload
{
    [TLObject(536919235)]
    public class TLRequestGetCdnFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 536919235;
            }
        }

        public byte[] file_token { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public Upload.TLAbsCdnFile Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            file_token = BytesUtil.Deserialize(br);
            offset = br.ReadInt32();
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(file_token, bw);
            bw.Write(offset);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Upload.TLAbsCdnFile)ObjectUtils.DeserializeObject(br);

        }
    }
}
