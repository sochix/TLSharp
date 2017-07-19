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

        public TLAbsRichText text { get; set; }
        public string url { get; set; }
        public long webpage_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            url = StringUtil.Deserialize(br);
            webpage_id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(text, bw);
            StringUtil.Serialize(url, bw);
            bw.Write(webpage_id);

        }
    }
}
