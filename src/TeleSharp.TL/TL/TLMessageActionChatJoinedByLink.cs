using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-123931160)]
    public class TLMessageActionChatJoinedByLink : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -123931160;
            }
        }

        public int InviterId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            InviterId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(InviterId);

        }
    }
}
