using System.IO;

namespace TeleSharp.TL
{
    [TLObject(524838915)]
    public class TLExportedMessageLink : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 524838915;
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