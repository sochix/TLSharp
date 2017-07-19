using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1642487306)]
    public class TLMessageService : TLAbsMessage
    {
        public override int Constructor
        {
            get
            {
                return -1642487306;
            }
        }

        public int flags { get; set; }
        public bool @out { get; set; }
        public bool mentioned { get; set; }
        public bool media_unread { get; set; }
        public bool silent { get; set; }
        public bool post { get; set; }
        public int id { get; set; }
        public int? from_id { get; set; }
        public TLAbsPeer to_id { get; set; }
        public int? reply_to_msg_id { get; set; }
        public int date { get; set; }
        public TLAbsMessageAction action { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = @out ? (flags | 2) : (flags & ~2);
            flags = mentioned ? (flags | 16) : (flags & ~16);
            flags = media_unread ? (flags | 32) : (flags & ~32);
            flags = silent ? (flags | 8192) : (flags & ~8192);
            flags = post ? (flags | 16384) : (flags & ~16384);
            flags = from_id != null ? (flags | 256) : (flags & ~256);
            flags = reply_to_msg_id != null ? (flags | 8) : (flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            @out = (flags & 2) != 0;
            mentioned = (flags & 16) != 0;
            media_unread = (flags & 32) != 0;
            silent = (flags & 8192) != 0;
            post = (flags & 16384) != 0;
            id = br.ReadInt32();
            if ((flags & 256) != 0)
                from_id = br.ReadInt32();
            else
                from_id = null;

            to_id = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            if ((flags & 8) != 0)
                reply_to_msg_id = br.ReadInt32();
            else
                reply_to_msg_id = null;

            date = br.ReadInt32();
            action = (TLAbsMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);





            bw.Write(id);
            if ((flags & 256) != 0)
                bw.Write(from_id.Value);
            ObjectUtils.SerializeObject(to_id, bw);
            if ((flags & 8) != 0)
                bw.Write(reply_to_msg_id.Value);
            bw.Write(date);
            ObjectUtils.SerializeObject(action, bw);

        }
    }
}
