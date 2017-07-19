using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1444503762)]
    public class TLRequestEditChatAdmin : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1444503762;
            }
        }

        public int chat_id { get; set; }
        public TLAbsInputUser user_id { get; set; }
        public bool is_admin { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            is_admin = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
            ObjectUtils.SerializeObject(user_id, bw);
            BoolUtil.Serialize(is_admin, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
