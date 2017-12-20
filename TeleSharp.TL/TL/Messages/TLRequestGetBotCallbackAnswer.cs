using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-2130010132)]
    public class TLRequestGetBotCallbackAnswer : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2130010132;
            }
        }

        public byte[] Data { get; set; }

        public int Flags { get; set; }

        public bool Game { get; set; }

        public int MsgId { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public Messages.TLBotCallbackAnswer Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Game = (Flags & 2) != 0;
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            MsgId = br.ReadInt32();
            if ((Flags & 1) != 0)
                Data = BytesUtil.Deserialize(br);
            else
                Data = null;
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLBotCallbackAnswer)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(MsgId);
            if ((Flags & 1) != 0)
                BytesUtil.Serialize(Data, bw);
        }
    }
}
