using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(398898678)]
    public class TLSupport : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 398898678;
            }
        }

        public string phone_number { get; set; }
        public TLAbsUser user { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);
            user = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(phone_number, bw);
            ObjectUtils.SerializeObject(user, bw);

        }
    }
}
