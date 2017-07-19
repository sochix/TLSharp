using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1212395773)]
    public class TLInputMediaGifExternal : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 1212395773;
            }
        }

        public string url { get; set; }
        public string q { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            q = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            StringUtil.Serialize(q, bw);

        }
    }
}
