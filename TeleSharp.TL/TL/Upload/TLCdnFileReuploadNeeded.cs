using System.IO;
namespace TeleSharp.TL.Upload
{
    [TLObject(-290921362)]
    public class TLCdnFileReuploadNeeded : TLAbsCdnFile
    {
        public override int Constructor
        {
            get
            {
                return -290921362;
            }
        }

        public byte[] request_token { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            request_token = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(request_token, bw);

        }
    }
}
