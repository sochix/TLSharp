using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1318675378)]
    public class TLRequestGetEmojiKeywordsLanguages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1318675378;
            }
        }

        public TLVector<string> LangCodes { get; set; }
        public TLVector<TLEmojiLanguage> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            LangCodes = (TLVector<string>)ObjectUtils.DeserializeVector<string>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(LangCodes, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLEmojiLanguage>)ObjectUtils.DeserializeVector<TLEmojiLanguage>(br);

        }
    }
}
