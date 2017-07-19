using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-299124375)]
    public class TLUpdateDraftMessage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -299124375;
            }
        }

        public TLAbsPeer peer { get; set; }
        public TLAbsDraftMessage draft { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            draft = (TLAbsDraftMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            ObjectUtils.SerializeObject(draft, bw);

        }
    }
}
