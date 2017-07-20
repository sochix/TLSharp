using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-842892769)]
    public class TLPaymentSavedCredentialsCard : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -842892769;
            }
        }

        public string id { get; set; }
        public string title { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = StringUtil.Deserialize(br);
            title = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(id, bw);
            StringUtil.Serialize(title, bw);

        }
    }
}
