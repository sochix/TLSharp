using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-248621111)]
    public class TLRequestEditPhoto : TLMethod
    {
        public override int Constructor => -248621111;

        public TLAbsInputChannel channel { get; set; }
        public TLAbsInputChatPhoto photo { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel) ObjectUtils.DeserializeObject(br);
            photo = (TLAbsInputChatPhoto) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(photo, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates) ObjectUtils.DeserializeObject(br);
        }
    }
}