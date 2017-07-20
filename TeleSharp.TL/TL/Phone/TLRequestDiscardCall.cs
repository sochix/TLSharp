using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(2027164582)]
    public class TLRequestDiscardCall : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2027164582;
            }
        }

        public TLInputPhoneCall peer { get; set; }
        public int duration { get; set; }
        public TLAbsPhoneCallDiscardReason reason { get; set; }
        public long connection_id { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            duration = br.ReadInt32();
            reason = (TLAbsPhoneCallDiscardReason)ObjectUtils.DeserializeObject(br);
            connection_id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(duration);
            ObjectUtils.SerializeObject(reason, bw);
            bw.Write(connection_id);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
