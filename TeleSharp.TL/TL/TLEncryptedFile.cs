using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1248893260)]
    public class TLEncryptedFile : TLAbsEncryptedFile
    {
        public override int Constructor
        {
            get
            {
                return 1248893260;
            }
        }

        public long id { get; set; }
        public long access_hash { get; set; }
        public int size { get; set; }
        public int dc_id { get; set; }
        public int key_fingerprint { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            access_hash = br.ReadInt64();
            size = br.ReadInt32();
            dc_id = br.ReadInt32();
            key_fingerprint = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(access_hash);
            bw.Write(size);
            bw.Write(dc_id);
            bw.Write(key_fingerprint);

        }
    }
}
