using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(358154344)]
    public class TLDocumentAttributeFilename : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return 358154344;
            }
        }

        public string FileName { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            FileName = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(FileName, bw);

        }
    }
}
