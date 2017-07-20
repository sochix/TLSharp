using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-415938591)]
    public class TLUpdateBotCallbackQuery : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -415938591;
            }
        }

        public int flags { get; set; }
        public long query_id { get; set; }
        public int user_id { get; set; }
        public TLAbsPeer peer { get; set; }
        public int msg_id { get; set; }
        public long chat_instance { get; set; }
        public byte[] data { get; set; }
        public string game_short_name { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = data != null ? (flags | 1) : (flags & ~1);
            flags = game_short_name != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            query_id = br.ReadInt64();
            user_id = br.ReadInt32();
            peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            msg_id = br.ReadInt32();
            chat_instance = br.ReadInt64();
            if ((flags & 1) != 0)
                data = BytesUtil.Deserialize(br);
            else
                data = null;

            if ((flags & 2) != 0)
                game_short_name = StringUtil.Deserialize(br);
            else
                game_short_name = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(query_id);
            bw.Write(user_id);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(msg_id);
            bw.Write(chat_instance);
            if ((flags & 1) != 0)
                BytesUtil.Serialize(data, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(game_short_name, bw);

        }
    }
}
