using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1557277184)]
    public class TLMessageMediaWebPage : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return -1557277184;
            }
        }

        public TLAbsWebPage Webpage { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Webpage = (TLAbsWebPage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Webpage, bw);

        }
    }
}
