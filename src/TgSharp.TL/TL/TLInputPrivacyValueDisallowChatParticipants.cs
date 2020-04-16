using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-668769361)]
    public class TLInputPrivacyValueDisallowChatParticipants : TLAbsInputPrivacyRule
    {
        public override int Constructor
        {
            get
            {
                return -668769361;
            }
        }

        public TLVector<int> Chats { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Chats = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Chats, bw);
        }
    }
}
