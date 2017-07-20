using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1066346178)]
    public class TLPageBlockPreformatted : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1066346178;
            }
        }

        public TLAbsRichText text { get; set; }
        public string language { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            language = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(text, bw);
            StringUtil.Serialize(language, bw);

        }
    }
}
