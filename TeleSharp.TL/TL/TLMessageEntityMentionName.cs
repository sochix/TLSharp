using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(892193368)]
    public class TLMessageEntityMentionName : TLAbsMessageEntity
    {
        public override int Constructor
        {
            get
            {
                return 892193368;
            }
        }

        public int offset { get; set; }
        public int length { get; set; }
        public int user_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            offset = br.ReadInt32();
            length = br.ReadInt32();
            user_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(offset);
            bw.Write(length);
            bw.Write(user_id);

        }
    }
}
