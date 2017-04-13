using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1799878989)]
    public class TLRequestGetDialogs : TLMethod
    {
        public override int Constructor => 1799878989;

        public int offset_date { get; set; }
        public int offset_id { get; set; }
        public TLAbsInputPeer offset_peer { get; set; }
        public int limit { get; set; }
        public TLAbsDialogs Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            offset_date = br.ReadInt32();
            offset_id = br.ReadInt32();
            offset_peer = (TLAbsInputPeer) ObjectUtils.DeserializeObject(br);
            limit = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(offset_date);
            bw.Write(offset_id);
            ObjectUtils.SerializeObject(offset_peer, bw);
            bw.Write(limit);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsDialogs) ObjectUtils.DeserializeObject(br);
        }
    }
}