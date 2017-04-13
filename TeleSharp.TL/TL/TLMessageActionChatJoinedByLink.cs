using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-123931160)]
    public class TLMessageActionChatJoinedByLink : TLAbsMessageAction
    {
        public override int Constructor => -123931160;

        public int inviter_id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            inviter_id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(inviter_id);
        }
    }
}