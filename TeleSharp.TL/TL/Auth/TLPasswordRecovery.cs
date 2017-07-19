using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(326715557)]
    public class TLPasswordRecovery : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 326715557;
            }
        }

        public string email_pattern { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            email_pattern = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(email_pattern, bw);

        }
    }
}
