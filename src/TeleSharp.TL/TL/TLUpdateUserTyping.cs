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

        public int UserId { get; set; }
        public TLAbsSendMessageAction Action { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            Action = (TLAbsSendMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            ObjectUtils.SerializeObject(Action, bw);

        }
    }
}
