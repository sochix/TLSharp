using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-95482955)]
    public class TLInputFileBig : TLAbsInputFile
    {
        public override int Constructor
        {
            get
            {
                return -95482955;
            }
        }

        public long id { get; set; }
        public int parts { get; set; }
        public string name { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            parts = br.ReadInt32();
            name = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(parts);
            StringUtil.Serialize(name, bw);

        }
    }
}
