using System.IO;

namespace TeleSharp.TL
{
    [TLObject(358154344)]
    public class TLDocumentAttributeFilename : TLAbsDocumentAttribute
    {
        public override int Constructor => 358154344;

        public string file_name { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            file_name = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(file_name, bw);
        }
    }
}