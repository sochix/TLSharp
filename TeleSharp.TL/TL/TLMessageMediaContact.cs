using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1585262393)]
    public class TLMessageMediaContact : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 1585262393;
            }
        }

        public string phone_number { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int user_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);
            first_name = StringUtil.Deserialize(br);
            last_name = StringUtil.Deserialize(br);
            user_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(phone_number, bw);
            StringUtil.Serialize(first_name, bw);
            StringUtil.Serialize(last_name, bw);
            bw.Write(user_id);

        }
    }
}
