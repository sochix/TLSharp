using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-714643696)]
    public class TLChannelAdminLogEventActionParticipantToggleAdmin : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return -714643696;
            }
        }

        public TLAbsChannelParticipant PrevParticipant { get; set; }
        public TLAbsChannelParticipant NewParticipant { get; set; }


        public void ComputeFlags()
        {

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
