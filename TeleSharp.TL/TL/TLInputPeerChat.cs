using System.IO;

namespace TeleSharp.TL
{
    [TLObject(396093539)]
    public class TLInputPeerChat : TLAbsInputPeer
    {
        public override int Constructor => 396093539;

        public int chat_id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
        }
    }
}