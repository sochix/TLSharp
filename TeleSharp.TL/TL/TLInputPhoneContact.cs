using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-208488460)]
    public class TLInputPhoneContact : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -208488460;
            }
        }

        public long client_id { get; set; }
        public string phone { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            client_id = br.ReadInt64();
            phone = StringUtil.Deserialize(br);
            first_name = StringUtil.Deserialize(br);
            last_name = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(client_id);
            StringUtil.Serialize(phone, bw);
            StringUtil.Serialize(first_name, bw);
            StringUtil.Serialize(last_name, bw);

        }
    }
}
