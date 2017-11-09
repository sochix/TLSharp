using System.IO;
namespace TeleSharp.TL
{
    [TLObject(236446268)]
    public class TLPhotoSizeEmpty : TLAbsPhotoSize
    {
        public override int Constructor
        {
            get
            {
                return 236446268;
            }
        }

        public string type { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            type = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(type, bw);

        }
    }
}
