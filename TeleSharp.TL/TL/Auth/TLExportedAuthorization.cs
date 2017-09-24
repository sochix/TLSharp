using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(-543777747)]
    public class TLExportedAuthorization : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -543777747;
            }
        }

        public int id { get; set; }
        public byte[] bytes { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
            bytes = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            BytesUtil.Serialize(bytes, bw);
        }
    }
}