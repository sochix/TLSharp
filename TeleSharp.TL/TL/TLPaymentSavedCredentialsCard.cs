using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-842892769)]
    public class TLPaymentSavedCredentialsCard : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -842892769;
            }
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = StringUtil.Deserialize(br);
            Title = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Id, bw);
            StringUtil.Serialize(Title, bw);
        }
    }
}
