using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1690108678)]
    public class TLInputEncryptedFileUploaded : TLAbsInputEncryptedFile
    {
        public override int Constructor
        {
            get
            {
                return 1690108678;
            }
        }

        public long Id { get; set; }
        public int Parts { get; set; }
        public string Md5Checksum { get; set; }
        public int KeyFingerprint { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt64();
            Parts = br.ReadInt32();
            Md5Checksum = StringUtil.Deserialize(br);
            KeyFingerprint = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(Parts);
            StringUtil.Serialize(Md5Checksum, bw);
            bw.Write(KeyFingerprint);

        }
    }
}
