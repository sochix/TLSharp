using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(873977640)]
    public class TLInputPaymentCredentials : TLAbsInputPaymentCredentials
    {
        public override int Constructor
        {
            get
            {
                return 873977640;
            }
        }

        public int flags { get; set; }
        public bool save { get; set; }
        public TLDataJSON data { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = save ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            save = (flags & 1) != 0;
            data = (TLDataJSON)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(data, bw);

        }
    }
}
