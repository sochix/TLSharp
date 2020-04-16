using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-422036098)]
    public class TLChannelAdminLogEventActionParticipantToggleBan : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return -422036098;
            }
        }

        public TLAbsChannelParticipant PrevParticipant { get; set; }
        public TLAbsChannelParticipant NewParticipant { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PrevParticipant = (TLAbsChannelParticipant)ObjectUtils.DeserializeObject(br);
            NewParticipant = (TLAbsChannelParticipant)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PrevParticipant, bw);
            ObjectUtils.SerializeObject(NewParticipant, bw);
        }
    }
}
