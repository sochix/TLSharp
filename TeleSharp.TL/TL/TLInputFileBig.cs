using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-95482955)]
    public class TLInputFileBig : TLAbsInputFile
    {
        public override int Constructor => -95482955;

        public long id { get; set; }
        public int parts { get; set; }
        public string name { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            parts = br.ReadInt32();
            name = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(parts);
            StringUtil.Serialize(name, bw);
        }
    }
}