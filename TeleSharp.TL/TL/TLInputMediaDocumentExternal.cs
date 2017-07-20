using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-437690244)]
    public class TLInputMediaDocumentExternal : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -437690244;
            }
        }

        public string url { get; set; }
        public string caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            caption = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            StringUtil.Serialize(caption, bw);

        }
    }
}
