using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(-966677240)]
    public class TLRequestGetPassportConfig : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -966677240;
            }
        }

        public int Hash { get; set; }
        public Help.TLAbsPassportConfig Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Help.TLAbsPassportConfig)ObjectUtils.DeserializeObject(br);

        }
    }
}
