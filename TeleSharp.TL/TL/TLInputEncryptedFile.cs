using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1511503333)]
    public class TLInputEncryptedFile : TLAbsInputEncryptedFile
    {
        public override int Constructor
        {
            get
            {
                return 1511503333;
            }
        }

        public long id { get; set; }
        public long access_hash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            access_hash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(access_hash);

        }
    }
}
