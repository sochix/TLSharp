using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-2130010132)]
    public class TLRequestGetBotCallbackAnswer : TLMethod
    {
        public override int Constructor => -2130010132;

        public int flags { get; set; }
        public bool game { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public int msg_id { get; set; }
        public byte[] data { get; set; }
        public TLBotCallbackAnswer Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = game ? flags | 2 : flags & ~2;
            flags = data != null ? flags | 1 : flags & ~1;
        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            game = (flags & 2) != 0;
            peer = (TLAbsInputPeer) ObjectUtils.DeserializeObject(br);
            msg_id = br.ReadInt32();
            if ((flags & 1) != 0)
                data = BytesUtil.Deserialize(br);
            else
                data = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(msg_id);
            if ((flags & 1) != 0)
                BytesUtil.Serialize(data, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLBotCallbackAnswer) ObjectUtils.DeserializeObject(br);
        }
    }
}