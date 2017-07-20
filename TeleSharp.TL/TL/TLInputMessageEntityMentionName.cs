using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(546203849)]
    public class TLInputMessageEntityMentionName : TLAbsMessageEntity
    {
        public override int Constructor
        {
            get
            {
                return 546203849;
            }
        }

        public int offset { get; set; }
        public int length { get; set; }
        public TLAbsInputUser user_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            offset = br.ReadInt32();
            length = br.ReadInt32();
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(offset);
            bw.Write(length);
            ObjectUtils.SerializeObject(user_id, bw);

        }
    }
}
