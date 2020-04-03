using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-709641735)]
    public class TLEmojiKeyword : TLAbsEmojiKeyword
    {
        public override int Constructor
        {
            get
            {
                return -709641735;
            }
        }

        public string Keyword { get; set; }
        public TLVector<string> Emoticons { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Keyword = StringUtil.Deserialize(br);
            Emoticons = (TLVector<string>)ObjectUtils.DeserializeVector<string>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Keyword, bw);
            ObjectUtils.SerializeObject(Emoticons, bw);

        }
    }
}
