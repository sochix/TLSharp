using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(415997816)]
    public class TLInviteText : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 415997816;
            }
        }

        public string message { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            message = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(message, bw);
        }
    }
}