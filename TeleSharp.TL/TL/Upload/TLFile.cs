using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(157948117)]
    public class TLFile : TLAbsFile
    {
        public override int Constructor
        {
            get
            {
                return 157948117;
            }
        }

        public Storage.TLAbsFileType type { get; set; }
        public int mtime { get; set; }
        public byte[] bytes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            type = (Storage.TLAbsFileType)ObjectUtils.DeserializeObject(br);
            mtime = br.ReadInt32();
            bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(type, bw);
            bw.Write(mtime);
            BytesUtil.Serialize(bytes, bw);

        }
    }
}
