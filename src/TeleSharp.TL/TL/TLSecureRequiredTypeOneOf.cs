using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(41187252)]
    public class TLSecureRequiredTypeOneOf : TLAbsSecureRequiredType
    {
        public override int Constructor
        {
            get
            {
                return 41187252;
            }
        }

        public TLVector<TLAbsSecureRequiredType> Types { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Types = (TLVector<TLAbsSecureRequiredType>)ObjectUtils.DeserializeVector<TLAbsSecureRequiredType>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Types, bw);

        }
    }
}
