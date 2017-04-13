using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1694474197)]
    public class TLChats : TLObject
    {
        public override int Constructor => 1694474197;

        public TLVector<TLAbsChat> chats { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            chats = ObjectUtils.DeserializeVector<TLAbsChat>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(chats, bw);
        }
    }
}