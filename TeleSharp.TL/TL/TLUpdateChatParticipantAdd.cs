using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-364179876)]
    public class TLUpdateChatParticipantAdd : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -364179876;
            }
        }

        public int chat_id { get; set; }
        public int user_id { get; set; }
        public int inviter_id { get; set; }
        public int date { get; set; }
        public int version { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
            user_id = br.ReadInt32();
            inviter_id = br.ReadInt32();
            date = br.ReadInt32();
            version = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
            bw.Write(user_id);
            bw.Write(inviter_id);
            bw.Write(date);
            bw.Write(version);

        }
    }
}
