using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(623001124)]
    public class TLRequestGetWebPagePreview : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 623001124;
            }
        }

        public string Message { get; set; }
        public TLAbsMessageMedia Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Message = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Message, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);

        }
    }
}
