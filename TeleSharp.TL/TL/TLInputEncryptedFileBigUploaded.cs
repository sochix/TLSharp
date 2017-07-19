using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(767652808)]
    public class TLInputEncryptedFileBigUploaded : TLAbsInputEncryptedFile
    {
        public override int Constructor
        {
            get
            {
                return 767652808;
            }
        }

        public long id { get; set; }
        public int parts { get; set; }
        public int key_fingerprint { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            parts = br.ReadInt32();
            key_fingerprint = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(parts);
            bw.Write(key_fingerprint);

        }
    }
}
