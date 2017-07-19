using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1855224129)]
    public class TLUpdateChatAdmins : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1855224129;
            }
        }

        public int chat_id { get; set; }
        public bool enabled { get; set; }
        public int version { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
            enabled = BoolUtil.Deserialize(br);
            version = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
            BoolUtil.Serialize(enabled, bw);
            bw.Write(version);

        }
    }
}
