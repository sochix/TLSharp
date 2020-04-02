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

        public long Id { get; set; }
        public int Parts { get; set; }
        public string Name { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt64();
            Parts = br.ReadInt32();
            Name = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(Parts);
            StringUtil.Serialize(Name, bw);

        }
    }
}
