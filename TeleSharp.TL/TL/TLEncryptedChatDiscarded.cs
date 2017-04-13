using System.IO;

namespace TeleSharp.TL
{
    [TLObject(332848423)]
    public class TLEncryptedChatDiscarded : TLAbsEncryptedChat
    {
        public override int Constructor => 332848423;

        public int id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
        }
    }
}