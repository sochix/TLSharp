using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-57668565)]
    public class TLChatParticipantsForbidden : TLAbsChatParticipants
    {
        public override int Constructor
        {
            get
            {
                return -57668565;
            }
        }

        public int flags { get; set; }
        public int chat_id { get; set; }
        public TLAbsChatParticipant self_participant { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = self_participant != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            chat_id = br.ReadInt32();
            if ((flags & 1) != 0)
                self_participant = (TLAbsChatParticipant)ObjectUtils.DeserializeObject(br);
            else
                self_participant = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(chat_id);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(self_participant, bw);

        }
    }
}
