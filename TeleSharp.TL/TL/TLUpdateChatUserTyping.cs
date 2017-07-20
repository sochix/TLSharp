using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1704596961)]
    public class TLUpdateChatUserTyping : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1704596961;
            }
        }

        public int chat_id { get; set; }
        public int user_id { get; set; }
        public TLAbsSendMessageAction action { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
            user_id = br.ReadInt32();
            action = (TLAbsSendMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
            bw.Write(user_id);
            ObjectUtils.SerializeObject(action, bw);

        }
    }
}
