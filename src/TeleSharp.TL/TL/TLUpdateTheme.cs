using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2112423005)]
    public class TLUpdateTheme : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -2112423005;
            }
        }

        public TLTheme Theme { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Theme = (TLTheme)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Theme, bw);

        }
    }
}
