using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1336546578)]
    public class TLMessageActionChannelMigrateFrom : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -1336546578;
            }
        }

        public string title { get; set; }
        public int chat_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            title = StringUtil.Deserialize(br);
            chat_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(title, bw);
            bw.Write(chat_id);

        }
    }
}
