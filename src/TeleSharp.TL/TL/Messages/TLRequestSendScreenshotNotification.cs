using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-914493408)]
    public class TLRequestSendScreenshotNotification : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -914493408;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public int ReplyToMsgId { get; set; }
        public long RandomId { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            ReplyToMsgId = br.ReadInt32();
            RandomId = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(ReplyToMsgId);
            bw.Write(RandomId);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
