using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1125058340)]
    public class TLInputDocumentFileLocation : TLAbsInputFileLocation
    {
        public override int Constructor
        {
            get
            {
                return 1125058340;
            }
        }

        public long id { get; set; }
        public long access_hash { get; set; }
        public int version { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            access_hash = br.ReadInt64();
            version = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(access_hash);
            bw.Write(version);

        }
    }
}
