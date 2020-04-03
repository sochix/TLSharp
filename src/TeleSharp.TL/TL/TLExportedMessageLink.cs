using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1571494644)]
    public class TLExportedMessageLink : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1571494644;
            }
        }

        public string Link { get; set; }
        public string Html { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Link = StringUtil.Deserialize(br);
            Html = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Link, bw);
            StringUtil.Serialize(Html, bw);

        }
    }
}
