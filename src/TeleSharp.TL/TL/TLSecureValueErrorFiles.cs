using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1717706985)]
    public class TLSecureValueErrorFiles : TLAbsSecureValueError
    {
        public override int Constructor
        {
            get
            {
                return 1717706985;
            }
        }

        public TLAbsSecureValueType Type { get; set; }
        public TLVector<byte[]> FileHash { get; set; }
        public string Text { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Type = (TLAbsSecureValueType)ObjectUtils.DeserializeObject(br);
            FileHash = (TLVector<byte[]>)ObjectUtils.DeserializeVector<byte[]>(br);
            Text = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Type, bw);
            ObjectUtils.SerializeObject(FileHash, bw);
            StringUtil.Serialize(Text, bw);

        }
    }
}
