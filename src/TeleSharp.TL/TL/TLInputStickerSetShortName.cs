using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2044933984)]
    public class TLInputStickerSetShortName : TLAbsInputStickerSet
    {
        public override int Constructor
        {
            get
            {
                return -2044933984;
            }
        }

        public string ShortName { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            ShortName = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(ShortName, bw);

        }
    }
}
