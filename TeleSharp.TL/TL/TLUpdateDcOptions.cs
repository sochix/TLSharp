using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1906403213)]
    public class TLUpdateDcOptions : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1906403213;
            }
        }

        public TLVector<TLDcOption> dc_options { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            dc_options = (TLVector<TLDcOption>)ObjectUtils.DeserializeVector<TLDcOption>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(dc_options, bw);

        }
    }
}
