using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-709817306)]
    public class TLRequestGetEmojiURL : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -709817306;
            }
        }

        public string LangCode { get; set; }
        public TLEmojiURL Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            LangCode = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(LangCode, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLEmojiURL)ObjectUtils.DeserializeObject(br);

        }
    }
}
