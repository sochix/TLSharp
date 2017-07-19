using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(568808380)]
    public class TLWebFile : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 568808380;
            }
        }

        public int size { get; set; }
        public string mime_type { get; set; }
        public Storage.TLAbsFileType file_type { get; set; }
        public int mtime { get; set; }
        public byte[] bytes { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            size = br.ReadInt32();
            mime_type = StringUtil.Deserialize(br);
            file_type = (Storage.TLAbsFileType)ObjectUtils.DeserializeObject(br);
            mtime = br.ReadInt32();
            bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(size);
            StringUtil.Serialize(mime_type, bw);
            ObjectUtils.SerializeObject(file_type, bw);
            bw.Write(mtime);
            BytesUtil.Serialize(bytes, bw);

        }
    }
}
