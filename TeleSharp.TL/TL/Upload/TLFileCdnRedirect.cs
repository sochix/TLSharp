using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(352864346)]
    public class TLFileCdnRedirect : TLAbsFile
    {
        public override int Constructor
        {
            get
            {
                return 352864346;
            }
        }

        public int dc_id { get; set; }
        public byte[] file_token { get; set; }
        public byte[] encryption_key { get; set; }
        public byte[] encryption_iv { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            dc_id = br.ReadInt32();
            file_token = BytesUtil.Deserialize(br);
            encryption_key = BytesUtil.Deserialize(br);
            encryption_iv = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(dc_id);
            BytesUtil.Serialize(file_token, bw);
            BytesUtil.Serialize(encryption_key, bw);
            BytesUtil.Serialize(encryption_iv, bw);

        }
    }
}
