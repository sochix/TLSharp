using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(1181279933)]
    public class TLAppChangelog : TLAbsAppChangelog
    {
        public override int Constructor => 1181279933;

        public string text { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(text, bw);
        }
    }
}