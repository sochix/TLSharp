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

        public string Title { get; set; }
        public int ChatId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Title = StringUtil.Deserialize(br);
            ChatId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Title, bw);
            bw.Write(ChatId);

        }
    }
}
