using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2103600678)]
    public class TLSecureRequiredType : TLAbsSecureRequiredType
    {
        public override int Constructor
        {
            get
            {
                return -2103600678;
            }
        }

        public int Flags { get; set; }
        public bool NativeNames { get; set; }
        public bool SelfieRequired { get; set; }
        public bool TranslationRequired { get; set; }
        public TLAbsSecureValueType Type { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            NativeNames = (Flags & 1) != 0;
            SelfieRequired = (Flags & 2) != 0;
            TranslationRequired = (Flags & 4) != 0;
            Type = (TLAbsSecureValueType)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



            ObjectUtils.SerializeObject(Type, bw);

        }
    }
}
