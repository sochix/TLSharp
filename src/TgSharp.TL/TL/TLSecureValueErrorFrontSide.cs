using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(12467706)]
    public class TLSecureValueErrorFrontSide : TLAbsSecureValueError
    {
        public override int Constructor
        {
            get
            {
                return 12467706;
            }
        }

        public TLAbsSecureValueType Type { get; set; }
        public byte[] FileHash { get; set; }
        public string Text { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Type = (TLAbsSecureValueType)ObjectUtils.DeserializeObject(br);
            FileHash = BytesUtil.Deserialize(br);
            Text = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Type, bw);
            BytesUtil.Serialize(FileHash, bw);
            StringUtil.Serialize(Text, bw);
        }
    }
}
