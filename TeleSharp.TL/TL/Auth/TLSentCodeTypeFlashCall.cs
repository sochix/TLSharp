using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(-1425815847)]
    public class TLSentCodeTypeFlashCall : TLAbsSentCodeType
    {
        public override int Constructor => -1425815847;

        public string pattern { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            pattern = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(pattern, bw);
        }
    }
}