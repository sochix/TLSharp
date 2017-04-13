using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-993483427)]
    public class TLRequestGetMessagesViews : TLMethod
    {
        public override int Constructor => -993483427;

        public TLAbsInputPeer peer { get; set; }
        public TLVector<int> id { get; set; }
        public bool increment { get; set; }
        public TLVector<int> Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer) ObjectUtils.DeserializeObject(br);
            id = ObjectUtils.DeserializeVector<int>(br);
            increment = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            ObjectUtils.SerializeObject(id, bw);
            BoolUtil.Serialize(increment, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = ObjectUtils.DeserializeVector<int>(br);
        }
    }
}