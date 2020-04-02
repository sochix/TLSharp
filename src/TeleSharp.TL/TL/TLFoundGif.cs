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

        public string Url { get; set; }
        public string ThumbUrl { get; set; }
        public string ContentUrl { get; set; }
        public string ContentType { get; set; }
        public int W { get; set; }
        public int H { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Url = StringUtil.Deserialize(br);
            ThumbUrl = StringUtil.Deserialize(br);
            ContentUrl = StringUtil.Deserialize(br);
            ContentType = StringUtil.Deserialize(br);
            W = br.ReadInt32();
            H = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Url, bw);
            StringUtil.Serialize(ThumbUrl, bw);
            StringUtil.Serialize(ContentUrl, bw);
            StringUtil.Serialize(ContentType, bw);
            bw.Write(W);
            bw.Write(H);

        }
    }
}
