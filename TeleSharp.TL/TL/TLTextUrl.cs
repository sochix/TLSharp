using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1009288385)]
    public class TLTextUrl : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return 1009288385;
            }
        }

        public TLAbsRichText Text { get; set; }
        public string Url { get; set; }
        public long WebpageId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            Url = StringUtil.Deserialize(br);
            WebpageId = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Text, bw);
            StringUtil.Serialize(Url, bw);
            bw.Write(WebpageId);

        }
    }
}
