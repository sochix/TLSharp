using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(314130811)]
    public class TLUpdateUserPhone : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 314130811;
            }
        }

        public int user_id { get; set; }
        public string phone { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            phone = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            StringUtil.Serialize(phone, bw);

        }
    }
}
