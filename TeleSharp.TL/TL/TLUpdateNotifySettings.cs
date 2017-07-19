using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1094555409)]
    public class TLUpdateNotifySettings : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1094555409;
            }
        }

        public TLAbsNotifyPeer peer { get; set; }
        public TLAbsPeerNotifySettings notify_settings { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsNotifyPeer)ObjectUtils.DeserializeObject(br);
            notify_settings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            ObjectUtils.SerializeObject(notify_settings, bw);

        }
    }
}
