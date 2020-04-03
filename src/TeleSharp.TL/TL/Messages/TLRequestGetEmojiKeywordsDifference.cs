using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(352892591)]
    public class TLRequestGetEmojiKeywordsDifference : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 352892591;
            }
        }

        public string LangCode { get; set; }
        public int FromVersion { get; set; }
        public TLEmojiKeywordsDifference Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            LangCode = StringUtil.Deserialize(br);
            FromVersion = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(LangCode, bw);
            bw.Write(FromVersion);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLEmojiKeywordsDifference)ObjectUtils.DeserializeObject(br);

        }
    }
}
