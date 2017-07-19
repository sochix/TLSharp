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

        public long id { get; set; }
        public int parts { get; set; }
        public string md5_checksum { get; set; }
        public int key_fingerprint { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            parts = br.ReadInt32();
            md5_checksum = StringUtil.Deserialize(br);
            key_fingerprint = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(parts);
            StringUtil.Serialize(md5_checksum, bw);
            bw.Write(key_fingerprint);

        }
    }
}
