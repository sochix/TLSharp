using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(372165663)]
    public class TLFoundGif : TLAbsFoundGif
    {
        public override int Constructor
        {
            get
            {
                return 372165663;
            }
        }

        public string url { get; set; }
        public string thumb_url { get; set; }
        public string content_url { get; set; }
        public string content_type { get; set; }
        public int w { get; set; }
        public int h { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            thumb_url = StringUtil.Deserialize(br);
            content_url = StringUtil.Deserialize(br);
            content_type = StringUtil.Deserialize(br);
            w = br.ReadInt32();
            h = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            StringUtil.Serialize(thumb_url, bw);
            StringUtil.Serialize(content_url, bw);
            StringUtil.Serialize(content_type, bw);
            bw.Write(w);
            bw.Write(h);

        }
    }
}
