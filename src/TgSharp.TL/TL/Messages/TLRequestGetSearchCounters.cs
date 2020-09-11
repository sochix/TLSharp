using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Messages
{
    [TLObject(1932455680)]
    public class TLRequestGetSearchCounters : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1932455680;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public TLVector<TLAbsMessagesFilter> Filters { get; set; }
        public TLVector<Messages.TLSearchCounter> Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Filters = (TLVector<TLAbsMessagesFilter>)ObjectUtils.DeserializeVector<TLAbsMessagesFilter>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            ObjectUtils.SerializeObject(Filters, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<Messages.TLSearchCounter>)ObjectUtils.DeserializeVector<Messages.TLSearchCounter>(br);
        }
    }
}
