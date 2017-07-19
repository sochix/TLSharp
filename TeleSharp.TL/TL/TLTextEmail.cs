using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-564523562)]
    public class TLTextEmail : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return -564523562;
            }
        }

        public TLAbsRichText text { get; set; }
        public string email { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            email = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(text, bw);
            StringUtil.Serialize(email, bw);

        }
    }
}
