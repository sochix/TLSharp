using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(779755552)]
    public class TLRequestReuploadCdnFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 779755552;
            }
        }

        public byte[] file_token { get; set; }
        public byte[] request_token { get; set; }
        public bool Response { get; set; }


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
            Response = BoolUtil.Deserialize(br);

        }
    }
}
