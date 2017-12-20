using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1008755359)]
    public class TLInlineBotSwitchPM : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1008755359;
            }
        }

        public string StartParam { get; set; }

        public string Text { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = StringUtil.Deserialize(br);
            StartParam = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Text, bw);
            StringUtil.Serialize(StartParam, bw);
        }
    }
}
