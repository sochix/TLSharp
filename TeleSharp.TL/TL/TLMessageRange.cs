using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(182649427)]
    public class TLMessageRange : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 182649427;
            }
        }

        public int min_id { get; set; }
        public int max_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            min_id = br.ReadInt32();
            max_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(min_id);
            bw.Write(max_id);

        }
    }
}
