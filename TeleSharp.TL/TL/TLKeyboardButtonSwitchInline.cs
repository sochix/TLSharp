using System.IO;

namespace TeleSharp.TL
{
    [TLObject(90744648)]
    public class TLKeyboardButtonSwitchInline : TLAbsKeyboardButton
    {
        public override int Constructor
        {
            get
            {
                return 90744648;
            }
        }

        public int Flags { get; set; }

        public string Query { get; set; }

        public bool SamePeer { get; set; }

        public string Text { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            SamePeer = (Flags & 1) != 0;
            Text = StringUtil.Deserialize(br);
            Query = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            StringUtil.Serialize(Text, bw);
            StringUtil.Serialize(Query, bw);
        }
    }
}
