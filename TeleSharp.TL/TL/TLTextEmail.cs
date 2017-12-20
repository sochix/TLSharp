using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-564523562)]
    public class TLTextEmail : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return -564523562;
            }
        }

        public string Email { get; set; }

        public TLAbsRichText Text { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            Email = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Text, bw);
            StringUtil.Serialize(Email, bw);
        }
    }
}
