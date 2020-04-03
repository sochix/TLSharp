using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(771095562)]
    public class TLChannelAdminLogEventActionDefaultBannedRights : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 771095562;
            }
        }

        public TLChatBannedRights PrevBannedRights { get; set; }
        public TLChatBannedRights NewBannedRights { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            PrevBannedRights = (TLChatBannedRights)ObjectUtils.DeserializeObject(br);
            NewBannedRights = (TLChatBannedRights)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PrevBannedRights, bw);
            ObjectUtils.SerializeObject(NewBannedRights, bw);

        }
    }
}
