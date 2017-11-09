using System.IO;
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

        public TLAbsInputPeer peer { get; set; }
        public int reply_to_msg_id { get; set; }
        public long random_id { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            reply_to_msg_id = br.ReadInt32();
            random_id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(reply_to_msg_id);
            bw.Write(random_id);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
