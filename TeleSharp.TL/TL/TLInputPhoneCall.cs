using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(506920429)]
    public class TLInputPhoneCall : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 506920429;
            }
        }

        public long id { get; set; }
        public long access_hash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            access_hash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(access_hash);

        }
    }
}
