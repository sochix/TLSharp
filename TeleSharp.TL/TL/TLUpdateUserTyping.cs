using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1548249383)]
    public class TLUpdateUserTyping : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1548249383;
            }
        }

        public int user_id { get; set; }
        public TLAbsSendMessageAction action { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            action = (TLAbsSendMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            ObjectUtils.SerializeObject(action, bw);

        }
    }
}
