using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1970352846)]
    public class TLStickers : TLAbsStickers
    {
        public override int Constructor
        {
            get
            {
                return -1970352846;
            }
        }

        public string hash { get; set; }
        public TLVector<TLAbsDocument> stickers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = StringUtil.Deserialize(br);
            stickers = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(hash, bw);
            ObjectUtils.SerializeObject(stickers, bw);

        }
    }
}
