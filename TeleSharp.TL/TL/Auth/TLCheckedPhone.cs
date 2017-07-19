using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-2128698738)]
    public class TLCheckedPhone : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -2128698738;
            }
        }

        public bool phone_registered { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_registered = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BoolUtil.Serialize(phone_registered, bw);

        }
    }
}
