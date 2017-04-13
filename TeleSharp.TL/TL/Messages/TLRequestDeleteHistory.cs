using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(469850889)]
    public class TLRequestDeleteHistory : TLMethod
    {
        public override int Constructor => 469850889;

        public int flags { get; set; }
        public bool just_clear { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public int max_id { get; set; }
        public TLAffectedHistory Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = just_clear ? flags | 1 : flags & ~1;
        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            just_clear = (flags & 1) != 0;
            peer = (TLAbsInputPeer) ObjectUtils.DeserializeObject(br);
            max_id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(max_id);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAffectedHistory) ObjectUtils.DeserializeObject(br);
        }
    }
}