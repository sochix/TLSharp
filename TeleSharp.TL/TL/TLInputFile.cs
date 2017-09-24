using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-181407105)]
    public class TLInputFile : TLAbsInputFile
    {
        public override int Constructor
        {
            get
            {
                return -181407105;
            }
        }

        public long id { get; set; }
        public int parts { get; set; }
        public string name { get; set; }
        public string md5_checksum { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            parts = br.ReadInt32();
            name = StringUtil.Deserialize(br);
            md5_checksum = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(parts);
            StringUtil.Serialize(name, bw);
            StringUtil.Serialize(md5_checksum, bw);
        }
    }
}