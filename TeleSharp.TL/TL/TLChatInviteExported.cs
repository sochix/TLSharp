using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-64092740)]
    public class TLChatInviteExported : TLAbsExportedChatInvite
    {
        public override int Constructor
        {
            get
            {
                return -64092740;
            }
        }

        public string link { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            link = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(link, bw);
        }
    }
}